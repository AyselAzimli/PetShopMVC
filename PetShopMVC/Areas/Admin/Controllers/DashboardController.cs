using Microsoft.AspNetCore.Mvc;

namespace PetShopMVC.Areas.Admin.Controllers
{
    public class DashboardController : AdminController
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
