using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;
using WebAppAss.Models;

namespace WebAppAss.Pages.Menu.Side
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

        public Models.Side Side { get; set; }

        public async Task<IActionResult> OnGetAsync(string name)
        {
            if (string.IsNullOrEmpty(name) || _context.Sides == null)
            {
                return NotFound();
            }

            Side = await _context.Sides.FirstOrDefaultAsync(m => m.Slug == name);
            if (Side == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostBuyAsync(int Id, int quantity)
        {
            Side = await _context.Sides.FirstOrDefaultAsync(b => b.Id == Id);
            if (Side == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            await _basketService.AddItemToBasketAsync(user, Id, quantity, "Side");

            return RedirectToPage("/Menu");
        }
    }
}
