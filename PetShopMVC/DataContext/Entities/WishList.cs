namespace PetShopMVC.DataContext.Entities
{
    public class WishList:BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
