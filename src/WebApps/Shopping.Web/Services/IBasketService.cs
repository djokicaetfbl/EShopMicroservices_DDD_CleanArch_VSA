using System.Net;

namespace Shopping.Web.Services
{
    public interface IBasketService
    {
        [Get("/basket-service/basket/{userName}")]
        Task<GetBasketResponse> GetBasket(string userName);

        [Post("/basket-service/basket")]
        Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

        [Delete("/basket-service/basket/{userName}")]
        Task<DeleteBasketResponse> DeleteBasket(string userName);

        [Post("/basket-service/basket/checkout")]
        Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest request);

        public async Task<ShoppingCartModel> LoadUserBasket() // defaultni interfejs
        {
            var userName = "swn";
            ShoppingCartModel basket;
            try
            {
                var getBasketResponse = await GetBasket(userName);
                basket = getBasketResponse.Cart;
            }
            catch (ApiException apiException) when (apiException.StatusCode == HttpStatusCode.NotFound)
            {
                basket = new ShoppingCartModel
                {
                    UserName = userName,
                    Items = []
                };
            }
            return basket;
        }

//        Bolja alternativa: Extension Methods
//Umesto default metode unutar interfejsa, mnogo je čistije i uobičajenije u.NET svetu koristiti Extension Methods.

//C#
//public static class BasketServiceExtensions
//        {
//            public static async Task<ShoppingCartModel> LoadUserBasket(this IBasketService basketService)
//            {
//                var userName = "swn";
//                try
//                {
//                    var response = await basketService.GetBasket(userName);
//                    return response.Cart;
//                }
//                catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
//                {
//                    return new ShoppingCartModel { UserName = userName, Items = [] };
//                }
//            }
//        }
//        Prednosti Extension metode:

//Radi sa Refit-om bez ikakvih nuspojava.

//Dostupna je svuda gde imaš IBasketService (samo kucaš .LoadUserBasket()).

//Čuva interfejs čistim(samo definicije ruta).

//Zaključak
//Tvoj kod će se kompajlirati i raditi ako koristiš C# 8.0+, ali ako planiraš da ovu metodu koristiš na više mesta, Extension Method je stabilnije rešenje za Refit klijente.
    }
}
