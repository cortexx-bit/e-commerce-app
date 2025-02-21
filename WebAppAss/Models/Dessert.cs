using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAppAss.Models
{
    public class Dessert : MenuItem
    {

        [Display(Name = "Warm Dessert")]
        public bool IsWarmDessert { get; set; }
    }
}