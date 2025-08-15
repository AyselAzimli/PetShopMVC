using Microsoft.EntityFrameworkCore;
using PetShopMVC.DataContext.Entities;

namespace PetShopMVC.Models
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; } = [];
        public List<Product> Products { get; set; } = [];
        public List<ProductTag> ProductTags { get; set; } = [];
        public List<ProductImage> ProductImages { get; set; } = [];
        public List<Slider> Sliders { get; set; } = [];
        public List<Tag> Tags { get; set; } = [];
        public List<Product> PetClothingProducts { get; set; } = [];
        public List<Product> PetFoodProducts { get; set; } = [];

    }
}
