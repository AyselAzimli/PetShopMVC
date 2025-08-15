namespace PetShopMVC.DataContext.Entities
{
    public class Bio:BaseEntity
    {
        public string LogoUrl { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
