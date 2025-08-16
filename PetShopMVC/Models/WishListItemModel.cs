namespace PetShopMVC.Models
{
    public class WishListItemModel
    {
		public int ProductId { get; set; }
		public string Name { get; set; } = null!;
		public int Count { get; set; }
		public decimal Price { get; set; }
	}
}
