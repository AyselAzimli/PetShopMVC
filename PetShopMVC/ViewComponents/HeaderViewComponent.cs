using Microsoft.AspNetCore.Mvc;
using PetShopMVC.DataContext;
using PetShopMVC.Models;

namespace PetShopMVC.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public HeaderViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var socials = _dbContext.Socials.ToList();
            var bios = _dbContext.Bios.FirstOrDefault();
            var sliders = _dbContext.Sliders.ToList();
            var model = new HeaderViewModel
            {
                Socials = socials,
                Bios = bios,
                Sliders=sliders
            };
            return View(model);
        }
    }
}
