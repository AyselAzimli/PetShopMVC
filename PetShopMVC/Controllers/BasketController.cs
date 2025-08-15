using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;
using PetShopMVC.DataContext;
using PetShopMVC.Models;

namespace PetShopMVC.Controllers
{
    public class BasketController : Controller
    {
        private const string BASKET_KEY = "basket";

        private readonly AppDbContext _dbContext;

        public BasketController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AddToBasket(int id)
        {
            var product = _dbContext.Products.Find(id);

            if (product == null) return BadRequest();

            var basketItems = AddBasketItemToBasket(id);

            var basketItemsInJson = JsonConvert.SerializeObject(basketItems);

            Response.Cookies.Append(BASKET_KEY, basketItemsInJson, new CookieOptions { Expires = DateTimeOffset.Now.AddDays(1) });

            var basketViewModel = GetBasketViewModelFromCookie(basketItems);

            return Json(basketViewModel);
        }

        public IActionResult InitBasket()
        {
            var basketItemsFromCookie = GetBasketItems();
            var basketViewModel = GetBasketViewModelFromCookie(basketItemsFromCookie);

            return Json(basketViewModel);
        }

        public IActionResult RemoveFromBasket(int id)
        {
            var basketItemsFromCookie = RemoveBasketItemFromBasket(id);

            var basketItemsInJson = JsonConvert.SerializeObject(basketItemsFromCookie);

            Response.Cookies.Append(BASKET_KEY, basketItemsInJson, new CookieOptions { Expires = DateTimeOffset.Now.AddDays(1) });

            var basketViewModel = GetBasketViewModelFromCookie(basketItemsFromCookie);

            return Json(basketViewModel);

        }

        public List<BasketCookieItemModel> GetBasketItems()
        {
            var basketItemsInString = Request.Cookies[BASKET_KEY];

            var basketItems = new List<BasketCookieItemModel>();

            if (!string.IsNullOrEmpty(basketItemsInString))
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketCookieItemModel>>(basketItemsInString);
            }

            return basketItems;
        }

        

        public List<BasketCookieItemModel> AddBasketItemToBasket(int id)
        {
            var basketItems = GetBasketItems();

            var existBasketItem = basketItems.Find(x => x.ProductId == id);

            if (existBasketItem == null)
                basketItems.Add(new BasketCookieItemModel { ProductId = id });
            else
                existBasketItem.Count++;

            return basketItems!;
        }

        public List<BasketCookieItemModel> RemoveBasketItemFromBasket(int id)
        {
            var basketItemsFromCookie = GetBasketItems();

            var itemIndex = basketItemsFromCookie.FindIndex(x => x.ProductId == id);

            if (itemIndex != -1)
                basketItemsFromCookie.RemoveAt(itemIndex);

            return basketItemsFromCookie;
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
