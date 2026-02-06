namespace Shopping.Web.Pages
{
    public class IndexModel(IBasketService basketService ,ICatalogService catalogService, ILogger<IndexModel> logger) : PageModel
    {
        public IEnumerable<ProductModel> ProductList { get; set; } = new List<ProductModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogInformation("Index page visisted");
            var result = await catalogService.GetProducts(); // poziva se preko Reffit-a
            //var result = await catalogService.GetProducts(2, 3); // poziva se preko Reffit-a
            ProductList = result.Products;
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            logger.LogInformation("Add to cart button clicked for productId: {ProductId}", productId);

            var productResponse = await catalogService.GetProduct(productId);

            var basket = await basketService.LoadUserBasket();
            
            basket.Items.Add(new ShoppingCartItemModel
            {
                ProductId = productId,
                ProductName = productResponse.Product.Name,
                Price = productResponse.Product.Price,
                Quantity = 1,
                Color = "Black"
            });

            await basketService.StoreBasket(new StoreBasketRequest(basket));

            return RedirectToPage("Cart");
        }
    }
}
