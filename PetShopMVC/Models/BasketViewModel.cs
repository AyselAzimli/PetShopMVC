namespace PetShopMVC.Models
{
    public class BasketViewModel
    {
        public int Count { get; set; }
        public decimal Total { get; set; }
        public List<BasketItemViewModel> Items { get; set; } = [];
    }

    public class BasketItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
