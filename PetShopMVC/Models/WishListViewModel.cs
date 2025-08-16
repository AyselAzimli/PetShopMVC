using PetShopMVC.DataContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace PetShopMVC.Models
{
    public class WishListViewModel
    {
        public List<Product> Products { get; set; } = [];
        public List<WishList> WishLists { get; set; } = [];
    }
}
