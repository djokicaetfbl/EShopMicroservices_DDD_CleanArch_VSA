using Ordering.Domain.Enums;

namespace Ordering.Application.Dtos
{
    public record OrderDto // REKORDI SU ODLICNI ZA DTO KLASE
    (
        Guid Id,
        Guid CustomerId,
        string OrderName,
        AddressDto ShippingAddress,
        AddressDto BillingAddress,
        PaymentDto Payment,
        OrderStatus Status,
        decimal TotalPrice,
        List<OrderItemDto> OrderItems
    );
}
