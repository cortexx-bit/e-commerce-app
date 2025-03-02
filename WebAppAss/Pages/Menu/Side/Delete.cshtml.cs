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

namespace WebAppAss.Pages.Menu.Side
{
    [Authorize(Roles = "Admin")]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (Side == null || Side.Id == 0)
            {
                return NotFound();
            }

            try
            {
                var side = await _context.Sides.FindAsync(Side.Id);
                if (side == null)
                {
                    return NotFound();
                }
                _context.Sides.Remove(side);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting side with ID {SideId}", Side.Id);
                return RedirectToPage("./Error");
            }

            return RedirectToPage("./Index");
        }
    }
}
