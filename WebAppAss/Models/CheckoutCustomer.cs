using System.ComponentModel.DataAnnotations;

namespace WebAppAss.Models
{
    public class CheckoutCustomer
    {
        [Key]
        public string Email { get; set; }
        public int? BasketID { get; set; }
        public Basket Basket { get; set; }
    }
}