using MassTransit;
using Microsoft.FeatureManagement;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler(IPublishEndpoint publishEndpoint, IFeatureManager featureManager ,ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

            if(await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
                var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();
                await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken); //
            }

        }
    }
}

//Ovaj handler reaguje na MediatR domain event, i ako je uključena feature flag opcija → šalje integration event na RabbitMQ.
//Logika:
//Primio se domain event (OrderCreatedEvent)
//Loguje
//Provjerava OrderFullfilment feature flag
//Ako je upaljen → pretvara domain model u DTO
//Publikuje integration event preko MassTransit-a
