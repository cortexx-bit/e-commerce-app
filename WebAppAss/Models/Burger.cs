using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppAss.Models
{
    public class Burger : MenuItem
    {
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        [Display(Name = "Vegetarian")]
        public bool IsVegetarian { get; set; }

        [Display(Name = "Special")]
        public bool Special { get; set; }
        
    }
}
