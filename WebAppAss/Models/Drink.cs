using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAppAss.Models
{
    public class Drink : MenuItem
    {
        [Required(ErrorMessage = "Size is required")]
        public string Size { get; set; }

        [Display(Name = "Alcoholic Beverage")]
        public bool IsAlcoholic { get; set; }
    }
}