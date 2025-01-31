using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;

namespace WebAppAss.Pages.Menu.Dessert
{
    public class DeleteModel : PageModel
    {
        private readonly WebAppAss.Data.WebAppAssContext _context;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(WebAppAssContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
      public WebAppAss.Models.Dessert Dessert { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Desserts == null)
            {
                return NotFound();
            }

            var dessert = await _context.Desserts.FirstOrDefaultAsync(m => m.Id == id);

            if (dessert == null)
            {
                return NotFound();
            }
            else 
            {
                Dessert = dessert;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Desserts == null)
            {
                return NotFound();
            }

            try
            {
                var dessert = await _context.Desserts.FindAsync(id);
                if (dessert == null)
                {
                    return NotFound();
                }
                _context.Desserts.Remove(dessert);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting dessert with ID {DessertId}", id);
                return RedirectToPage("./Error");
            }

            return RedirectToPage("./Index");
        }
    }
}
