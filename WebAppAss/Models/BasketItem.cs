namespace WebAppAss.Models
{
    public class BasketItem
    {
        public int BasketID { get; set; }
        public int ItemID { get; set; }
        public string ItemType { get; set; }
        public int Quantity { get; set; }
        public Basket Basket { get; set; }
    }
}