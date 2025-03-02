using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;
using WebAppAss.Models;

namespace WebAppAss.Pages.Menu.Drink
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly WebAppAssContext _context;
        private readonly ILogger<DeleteModel> _logger;
        public DeleteModel(WebAppAssContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public WebAppAss.Models.Drink Drink { get; set; }

        public async Task<IActionResult> OnGetAsync(string name)
        {
            if (string.IsNullOrEmpty(name) || _context.Drinks == null)
            {
                return NotFound();
            }

            Drink = await _context.Drinks.FirstOrDefaultAsync(m => m.Slug == name);
            if (Drink == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Drink == null || Drink.Id == 0)
            {
                return NotFound();
            }

            try
            {
                var drink = await _context.Drinks.FindAsync(Drink.Id);
                if (drink == null) return NotFound();

                _context.Drinks.Remove(drink);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting drink with ID {DrinkId}", Drink.Id);
                return RedirectToPage("./Error");
            }

            return RedirectToPage("./Index");
        }
    }
}
