using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetShopMVC.DataContext;
using PetShopMVC.Models;

namespace PetShopMVC.ViewComponents
{

    public class CartViewComponent:ViewComponent
    {
        private const string BASKET_KEY = "basket";

        private readonly AppDbContext _dbContext;

        public CartViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var basketItemsFromCookie = GetBasketItems();
            var basketViewModel = GetBasketViewModelFromCookie(basketItemsFromCookie);

            return View(basketViewModel);
        }

        public List<BasketCookieItemModel> GetBasketItems()
        {
            var basketItemsInString = Request.Cookies[BASKET_KEY];

            var basketItems = new List<BasketCookieItemModel>();

            if (!string.IsNullOrEmpty(basketItemsInString))
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketCookieItemModel>>(basketItemsInString);
            }

            return basketItems!;
        }

        public BasketViewModel GetBasketViewModelFromCookie(List<BasketCookieItemModel> basketCookieItemModels)
        {
            var basketViewModel = new BasketViewModel();
            var basketItemViewModels = new List<BasketItemViewModel>();
            foreach (var item in basketCookieItemModels)
            {
                var product = _dbContext.Products.Find(item.ProductId);

                if (product == null) continue;

                basketItemViewModels.Add(new BasketItemViewModel
                {
                    Id = product.Id,
                    Price = product.Price,
                    Name = product.Name,
                    Count = item.Count,
                });
            }

            basketViewModel.Items = basketItemViewModels;
            basketViewModel.Count = basketItemViewModels.Sum(x => x.Count);
            basketViewModel.Total = basketItemViewModels.Sum(x => x.Count * x.Price);

            return basketViewModel;
        }

    }
}
