using PetShopMVC.DataContext.Entities;

namespace PetShopMVC.Models
{
    public class HeaderViewModel
    {
        public List<Social> Socials { get; set; } = [];
        public Bio? Bios { get; set; }
        public List<Slider> Sliders { get; set; } = [];

    }
}
