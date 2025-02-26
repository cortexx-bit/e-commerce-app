namespace WebAppAss.Models
{
    // View registered accounts in admin pages
    public class AdminUserView
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
