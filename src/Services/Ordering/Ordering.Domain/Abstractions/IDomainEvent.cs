using MediatR;

namespace Ordering.Domain.Abstractions
{
    public interface IDomainEvent : INotification // allowing MediatR to handle domain events
    {
        Guid EventId => Guid.NewGuid();
        public DateTime OccurredOn => DateTime.Now;
        public string EventType => GetType().AssemblyQualifiedName;
    }
}
