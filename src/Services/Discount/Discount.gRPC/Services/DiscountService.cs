using Discount.gRPC.Data;
using Discount.gRPC.Models;
using Discount.Grpc;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Services
{
    public class DiscountService(DiscountContext dbContex, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContex.Coupons
                .FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

            coupon ??= new Coupon // if (coupon is null) // null-coalescing assignment
            {
                    ProductName = "No Discount",
                    Description = "No Discount Desc",
                    Amount = 0
                };

            logger.LogInformation("Discount is retrieved for ProductName : {ProductName}, Amount : {Amount}",coupon.ProductName, coupon.Amount);

            return coupon.Adapt<CouponModel>();

        }

        public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>() ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon data is null"));
            dbContex.Coupons.Add(coupon);
            await dbContex.SaveChangesAsync();

            logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);

            return coupon.Adapt<CouponModel>();
        }

        public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>() ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon data is null"));
            dbContex.Coupons.Update(coupon);
            await dbContex.SaveChangesAsync();

            logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);

            return coupon.Adapt<CouponModel>();
        }

        public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContex.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
            
            dbContex.Coupons.Remove(coupon);
            await dbContex.SaveChangesAsync();

            logger.LogInformation("Discount is successfully deleted. ProductName : {ProductName}", coupon.ProductName);

            return new DeleteDiscountResponse { Success = true };
        }
    }
}
