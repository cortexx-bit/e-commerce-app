using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppAss.Models
{
    public class CheckoutItem
    {
        [Key, Required]
        public int ID { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }
        [Required, StringLength(50)]
        public string Item_Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string CompositeID { get; set; }

    }
}
