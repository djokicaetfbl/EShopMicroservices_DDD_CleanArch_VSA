namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            //Update Order entity from command object
            //save to base
            //return result

            var orderId = OrderId.Of(command.Order.Id);

            var order = await dbContext.Orders
                .FindAsync([orderId], cancellationToken: cancellationToken);

            if(order is null)
            {
                throw new OrderNotFoundException(command.Order.Id);
            }

            UpdateOrderWithNewValues(order, command.Order);

            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);


            return new UpdateOrderResult(true);
        }

        public void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
        {
            var updatedShippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName,
                    orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country,
                    orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);

            var updatedBillingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName,
                orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country,
                orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);

            var updatePayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardName,
                orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

            order.Update(orderName: OrderName.Of(orderDto.OrderName), // ovo je Domain layer (Ordering.Domain) i Application (Ordering.Application) po clean arhitekturi moze da koristi DomainLayer (sto je i slucaj u Dependencies Application se koristi projekat Ordering.Domain)
                shippingAddress: updatedShippingAddress,
                billingAddress: updatedBillingAddress,
                payment: updatePayment,
                orderStatus: orderDto.Status
                );
        }
    }
}
