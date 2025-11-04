namespace Ordering.Domain.Events
{
    public record OrderItemAddedEvent(Order order) : IDomainEvent
    {
    }
}
