namespace PetShopMVC.DataContext.Entities
{
    public class Tag:BaseEntity
    {
        public required string Name {  get; set; }
        public List<ProductTag> ProductsTags { get; set; } = [];
    }
}
