namespace WebAppAss.Models
{
    // View order history in admin pages
    public class AdminOrderView
    {
        public int OrderNo { get; set; }
        public string Email { get; set; }
        public List<OrderItemView> Items { get; set; }
    }
    public class OrderItemView
    {
        public int ItemID { get; set; }
        public string ItemType { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
    }

}
