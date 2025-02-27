using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;
using WebAppAss.Models;

namespace WebAppAss.Pages.Menu.Dessert
{
    public class DetailsModel : PageModel
    {
        private readonly WebAppAssContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IBasketService _basketService;

        public DetailsModel(WebAppAssContext context, UserManager<IdentityUser> userManager, IBasketService basketService)
        {
            _context = context;
            _userManager = userManager;
            _basketService = basketService;
        }

        public Models.Dessert Dessert { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Desserts == null)
            {
                return NotFound();
            }

            Dessert = await _context.Desserts.FirstOrDefaultAsync(m => m.Id == id);
            if (Dessert == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostBuyAsync(int Id, int quantity)
        {
            Dessert = await _context.Desserts.FirstOrDefaultAsync(b => b.Id == Id);
            if (Dessert == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            await _basketService.AddItemToBasketAsync(user, Id, quantity, "Dessert");

            return RedirectToPage("/Menu");
        }
    }
}
