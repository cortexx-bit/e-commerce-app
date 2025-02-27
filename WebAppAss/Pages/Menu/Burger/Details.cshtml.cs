using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;
using WebAppAss.Models;

namespace WebAppAss.Pages.Menu.Burger
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

        public Models.Burger Burger { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Burgers == null)
            {
                return NotFound();
            }

            Burger = await _context.Burgers.FirstOrDefaultAsync(m => m.Id == id);
            if (Burger == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostBuyAsync(int Id, int quantity)
        {
            Burger = await _context.Burgers.FirstOrDefaultAsync(b => b.Id == Id);
            if (Burger == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            await _basketService.AddItemToBasketAsync(user, Id, quantity, "Burger");

            return RedirectToPage("/Menu");
        }
    }
}
