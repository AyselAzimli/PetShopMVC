using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetShopMVC.DataContext;
using PetShopMVC.Models;

namespace PetShopMVC.Controllers
{
    public class WishListController : Controller
    {
        private const string WISHLIST_KEY = "wishList";
        private readonly AppDbContext _dbContext;

        public WishListController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var wishListCookie = Request.Cookies["wishList"];
            var wishListItems = string.IsNullOrEmpty(wishListCookie)
                ? new List<WishListCookieItemModel>()
                : JsonConvert.DeserializeObject<List<WishListCookieItemModel>>(wishListCookie);

            var products = _dbContext.Products
                .Where(p => wishListItems.Select(w => w.ProductId).Contains(p.Id))
                .ToList();

            var viewModel = new WishListViewModel
            {
                Products = products
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            var wishListCookie = Request.Cookies["wishList"];
            var wishListItems = string.IsNullOrEmpty(wishListCookie)
                ? new List<WishListCookieItemModel>()
                : JsonConvert.DeserializeObject<List<WishListCookieItemModel>>(wishListCookie);

            if (!wishListItems.Any(w => w.ProductId == id))
                wishListItems.Add(new WishListCookieItemModel { ProductId = id });

            Response.Cookies.Append("wishList", JsonConvert.SerializeObject(wishListItems));

            // Return current wishlist as JSON
            var products = _dbContext.Products
                .Where(p => wishListItems.Select(w => w.ProductId).Contains(p.Id))
                .ToList();

            return Json(new { Products = products });
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var wishListCookie = Request.Cookies["wishList"];
            var wishListItems = string.IsNullOrEmpty(wishListCookie)
                ? new List<WishListCookieItemModel>()
                : JsonConvert.DeserializeObject<List<WishListCookieItemModel>>(wishListCookie);

            wishListItems?.RemoveAll(w => w.ProductId == id);

            Response.Cookies.Append("wishList", JsonConvert.SerializeObject(wishListItems));

            var products = _dbContext.Products
                .Where(p => wishListItems.Select(w => w.ProductId).Contains(p.Id))
                .ToList();

            return Json(new { Products = products });
        }

        [HttpGet]
        public IActionResult InitWishList()
        {
            var wishListCookie = Request.Cookies[WISHLIST_KEY];
            var wishListItems = string.IsNullOrEmpty(wishListCookie)
                ? new List<WishListCookieItemModel>()
                : JsonConvert.DeserializeObject<List<WishListCookieItemModel>>(wishListCookie);

            var products = _dbContext.Products
                .Where(p => wishListItems.Select(w => w.ProductId).Contains(p.Id))
                .Select(p => new {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.CoverImageUrl
                })
                .ToList();

            return Json(new { Products = products });
        }

        [HttpGet]
        public IActionResult AddToWishList(int id)
        {
            var wishListCookie = Request.Cookies[WISHLIST_KEY];
            var wishListItems = string.IsNullOrEmpty(wishListCookie)
                ? new List<WishListCookieItemModel>()
                : JsonConvert.DeserializeObject<List<WishListCookieItemModel>>(wishListCookie);

            if (!wishListItems.Any(w => w.ProductId == id))
            {
                wishListItems.Add(new WishListCookieItemModel { ProductId = id });
                Response.Cookies.Append(WISHLIST_KEY, JsonConvert.SerializeObject(wishListItems));
            }

            // return updated products as JSON
            var products = _dbContext.Products
                .Where(p => wishListItems.Select(w => w.ProductId).Contains(p.Id))
                .Select(p => new {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.CoverImageUrl
                })
                .ToList();

            return Json(new { Products = products });
        }

        [HttpGet]
        public IActionResult RemoveFromWishList(int id)
        {
            var wishListCookie = Request.Cookies[WISHLIST_KEY];
            var wishListItems = string.IsNullOrEmpty(wishListCookie)
                ? new List<WishListCookieItemModel>()
                : JsonConvert.DeserializeObject<List<WishListCookieItemModel>>(wishListCookie);

            wishListItems.RemoveAll(w => w.ProductId == id);
            Response.Cookies.Append(WISHLIST_KEY, JsonConvert.SerializeObject(wishListItems));

            var products = _dbContext.Products
                .Where(p => wishListItems.Select(w => w.ProductId).Contains(p.Id))
                .Select(p => new {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.CoverImageUrl
                })
                .ToList();

            return Json(new { Products = products });
        }


    }
}
