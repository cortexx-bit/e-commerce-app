using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppAss.Pages.Admin
{
    [Authorize(Roles ="Admin")]
    public class MenuModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
