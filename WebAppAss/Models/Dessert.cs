using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAppAss.Models
{
    public class Dessert : MenuItem
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Warm Dessert")]
        public bool IsWarmDessert { get; set; }
    }
}