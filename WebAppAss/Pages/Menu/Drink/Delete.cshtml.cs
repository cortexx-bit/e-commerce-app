using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;
using WebAppAss.Models;

namespace WebAppAss.Pages.Menu.Drink
{
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Drinks == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks.FirstOrDefaultAsync(m => m.Id == id);

            if (drink == null)
            {
                return NotFound();
            }
            else 
            {
                Drink = drink;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            try
            {
                var drink = await _context.Drinks.FindAsync(id);
                if (drink == null) return NotFound();

                _context.Drinks.Remove(drink);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting drink with ID {DrinkId}", id);
                return RedirectToPage("./Error");
            }

            return RedirectToPage("./Index");
        }
    }
}
