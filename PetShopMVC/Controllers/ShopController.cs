using Microsoft.AspNetCore.Mvc;
using PetShopMVC.DataContext;
using PetShopMVC.Models;

namespace PetShopMVC.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ShopController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var products = _dbContext.Products.Take(3).ToList();

            ViewBag.ProductCount = _dbContext.Products.Count();

            if (products == null)
                return NotFound();

            var shopViewModel = new ShopViewModel
            {
                Products = products,

            };

            return View(shopViewModel);
        }

        public IActionResult Partial(int skip)
        {
            var products = _dbContext.Products.Skip(skip).Take(3).ToList();

            return PartialView("_ProductsPartialLoadMore", products);
        }
    }
}
