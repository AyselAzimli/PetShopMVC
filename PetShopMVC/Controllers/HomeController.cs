using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopMVC.DataContext;
using PetShopMVC.Models;

namespace PetShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {

            var categories = _dbContext.Categories.ToList();
            var productImages=_dbContext.ProductImages.ToList();
            var productTags=_dbContext.ProductTags.ToList();
            var sliders = _dbContext.Sliders.ToList();

            var products = _dbContext.Products.Include(p => p.Images).Include(p => p.Category).ToList();

            var petClothingProducts = _dbContext.Products.Include(p => p.Category)
            .Where(p => p.Category.Name == "Pet Clothing").ToList();

            var petFoodProducts = _dbContext.Products.Include(p => p.Category).Where(p => p.Category.Name == "Food").ToList();
            
            var homeViewModel = new HomeViewModel
            {
                Categories=categories,
                Products=products,
                ProductImages=productImages,
                ProductTags=productTags,
                Sliders=sliders,
                PetClothingProducts= petClothingProducts,
                PetFoodProducts= petFoodProducts
            };
            return View(homeViewModel);
        }
    }
}
