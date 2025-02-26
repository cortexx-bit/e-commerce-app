using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;
using WebAppAss.Models;

namespace WebAppAss.Pages.Menu.Drink
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

        public Models.Drink Drink { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Drinks == null)
            {
                return NotFound();
            }

            Drink = await _context.Drinks.FirstOrDefaultAsync(m => m.Id == id);
            if (Drink == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostBuyAsync(int Id, int quantity)
        {
            Drink = await _context.Drinks.FirstOrDefaultAsync(b => b.Id == Id);
            if (Drink == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            await _basketService.AddItemToBasketAsync(user, Id, quantity, "Drink");

            return RedirectToPage("/Menu");
        }
    }
}
