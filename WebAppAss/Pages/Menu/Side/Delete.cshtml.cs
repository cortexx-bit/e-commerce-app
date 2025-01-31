using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;
using WebAppAss.Models;

namespace WebAppAss.Pages.Menu.Side
{
    public class DeleteModel : PageModel
    {
        private readonly WebAppAss.Data.WebAppAssContext _context;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(WebAppAss.Data.WebAppAssContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
      public WebAppAss.Models.Side Side { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Sides == null)
            {
                return NotFound();
            }

            var side = await _context.Sides.FirstOrDefaultAsync(m => m.Id == id);

            if (side == null)
            {
                return NotFound();
            }
            else 
            {
                Side = side;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Sides == null)
            {
                return NotFound();
            }

            try
            {
                var side = await _context.Sides.FindAsync(id);
                if (side == null)
                {
                    return NotFound();
                }
                _context.Sides.Remove(side);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting side with ID {SideId}", id);
                return RedirectToPage("./Error");
            }

            return RedirectToPage("./Index");
        }
    }
}
