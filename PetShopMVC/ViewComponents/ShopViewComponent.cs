using Microsoft.AspNetCore.Mvc;
using PetShopMVC.DataContext;
using PetShopMVC.Models;

namespace PetShopMVC.ViewComponents
{
    public class ShopViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public ShopViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = _dbContext.Products.ToList();

            var model = new ShopViewModel
            {
                Products = products

            };
            return View(model);
        }
    }
}
