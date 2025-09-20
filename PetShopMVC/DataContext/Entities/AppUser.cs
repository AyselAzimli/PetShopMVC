using Microsoft.AspNetCore.Identity;

namespace PetShopMVC.DataContext.Entities
{
    public class AppUser:IdentityUser
    {
        public string? FullName { get; set; }
    }
}
