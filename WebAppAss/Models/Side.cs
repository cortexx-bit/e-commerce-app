using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAppAss.Models
{
    public class Side : MenuItem
    {
        [Required(ErrorMessage = "Size is required")]
        public string Size { get; set; }

    }
}