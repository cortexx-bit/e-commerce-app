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

namespace WebAppAss.Pages.Menu.Burger
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
      public WebAppAss.Models.Burger Burger { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Burgers == null)
            {
                return NotFound();
            }

            var burger = await _context.Burgers.FirstOrDefaultAsync(m => m.Id == id);

            if (burger == null)
            {
                return NotFound();
            }
            else 
            {
                Burger = burger;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Burgers == null)
            {
                return NotFound();
            }

            try
            {
                var burger = await _context.Burgers.FindAsync(id);
                if (burger == null)
                {
                    return NotFound();
                }
                _context.Burgers.Remove(burger);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting burger with ID {BurgerId}", id);
                return RedirectToPage("./Error");
            }

            return RedirectToPage("./Index");
        }
    }
}
