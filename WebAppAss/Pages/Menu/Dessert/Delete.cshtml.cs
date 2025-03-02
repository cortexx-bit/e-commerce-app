using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;

namespace WebAppAss.Pages.Menu.Dessert
{
    [Authorize(Roles = "Admin")]
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

        public async Task<IActionResult> OnGetAsync(string name)
        {
            if (string.IsNullOrEmpty(name) || _context.Desserts == null)
            {
                return NotFound();
            }

            Dessert = await _context.Desserts.FirstOrDefaultAsync(m => m.Slug == name);
            if (Dessert == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Dessert == null || Dessert.Id == 0)
            {
                return NotFound();
            }

            try
            {
                var dessert = await _context.Desserts.FindAsync(Dessert.Id);
                if (dessert == null)
                {
                    return NotFound();
                }
                _context.Desserts.Remove(dessert);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting dessert with ID {DessertId}", Dessert.Id);
                return RedirectToPage("./Error");
            }

            return RedirectToPage("./Index");
        }
    }
}
