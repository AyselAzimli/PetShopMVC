using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetShopMVC.DataContext.Entities;
using System.Data;

namespace PetShopMVC.DataContext
{
    public class DataInitializer
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataInitializer(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, AppDbContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task SeedData()
        {
            _dbContext.Database.Migrate(); //database update edir
            await CreateSuperAdmin();
        }
        
        public async Task CreateSuperAdmin()
        {
            List<string> roles = ["SuperAdmin","Admin", "Moderator", "User"];
            foreach (var role in roles)
            {
                var hasRole= await _roleManager.RoleExistsAsync(role);
                if(hasRole)
                {
                    continue;
                }

                await _roleManager.CreateAsync(new IdentityRole { Name = role });

            }

            var existUser = await _userManager.FindByNameAsync("superadmin");
            if (existUser != null)
                return;
            var superadmin = new AppUser
            {
                Email = "super@email",
                UserName = "superadmin"
            };

            var result=await _userManager.CreateAsync(superadmin,"12345");
            if(!result.Succeeded) return;
            await _userManager.AddToRoleAsync(superadmin, "SuperAdmin");

        }
    }
}
