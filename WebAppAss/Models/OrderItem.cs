using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppAss.Models
{
    public class OrderItem
    {
        [Key, Column(Order =0)]
        [ForeignKey("OrderHistory")]
        public int OrderNo { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int ItemID { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public string ItemType { get; set; }
        public int Quantity { get; set; }
    }
}
