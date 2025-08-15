using Microsoft.EntityFrameworkCore;
using PetShopMVC.DataContext.Entities;

namespace PetShopMVC.DataContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Bio> Bios { get;set; }
        public DbSet<Social> Socials { get;set; }
        public DbSet<Category> Categories { get;set; }
        public DbSet<Product> Products { get;set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
