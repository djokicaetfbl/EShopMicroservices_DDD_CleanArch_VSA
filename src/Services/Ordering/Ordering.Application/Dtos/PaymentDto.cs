namespace Ordering.Application.Dtos
{
    public record PaymentDto
        (
            string? CardName,
            string CardNumber,
            string Expiration,
            string Cvv, // Mapster can't map Uppercase CVV property
            int PaymentMethod
        );
}