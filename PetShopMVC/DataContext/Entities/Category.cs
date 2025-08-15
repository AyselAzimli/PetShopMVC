namespace PetShopMVC.DataContext.Entities
{
    public class Category:BaseEntity
    {
        public required string Name { get; set; }       
        public string ImageUrl { get; set; } = null!;
        public string IconClass { get; set; } = null!;

        public List<Product> Products { get; set; } = [];
    }
}
