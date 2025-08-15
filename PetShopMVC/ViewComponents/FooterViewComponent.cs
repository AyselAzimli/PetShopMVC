using Microsoft.AspNetCore.Mvc;
using PetShopMVC.DataContext;
using PetShopMVC.Models;

namespace PetShopMVC.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public FooterViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var socials = _dbContext.Socials.ToList();
            var bios = _dbContext.Bios.FirstOrDefault();
            var model = new FooterViewModel
            {
                Socials = socials,
                Bios = bios,
               
            };
            return View(model);
        }
    }
}
