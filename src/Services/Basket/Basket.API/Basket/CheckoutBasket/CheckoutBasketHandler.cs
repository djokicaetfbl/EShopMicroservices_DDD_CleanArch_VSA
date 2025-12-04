using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);

    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto can not be null");
            RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required");
            //RuleFor(x => x.BasketCheckoutDto.CustomerId).NotEmpty().WithMessage("CustomerId is required");
            //RuleFor(x => x.BasketCheckoutDto.TotalPrice).GreaterThan(0).WithMessage("TotalPrice must be greater than zero");
        }
    }

    public class CheckoutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            //get existing basket with total price
            //Set total price onbasket checkout event message
            //send basket checkout event to rabbitmq using masstransit
            //delete basket

            var basket = await repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
            if(basket == null)
            {
                return new CheckoutBasketResult(IsSuccess: false);
            }

            var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;

            await publishEndpoint.Publish(eventMessage, cancellationToken);

            await repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);

            return new CheckoutBasketResult(IsSuccess: true);
        }
    }
}
