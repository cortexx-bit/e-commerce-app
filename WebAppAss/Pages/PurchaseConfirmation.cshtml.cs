using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppAss.Pages
{
    public class PurchaseConfirmationModel : PageModel
    {
        public int OrderNumber { get; set; }

        public void OnGet(int orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}