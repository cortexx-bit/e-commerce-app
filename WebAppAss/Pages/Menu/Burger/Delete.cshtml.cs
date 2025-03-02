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

        public async Task<IActionResult> OnGetAsync(string name)
        {
            if (string.IsNullOrEmpty(name) || _context.Burgers == null)
            {
                return NotFound();
            }

            Burger = await _context.Burgers.FirstOrDefaultAsync(m => m.Slug == name);
            if (Burger == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (Burger == null || Burger.Id == 0)
            {
                return NotFound();
            }

            try
            {
                var burger = await _context.Burgers.FindAsync(Burger.Id);
                if (burger == null)
                {
                    return NotFound();
                }
                _context.Burgers.Remove(burger);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting burger with ID {BurgerId}", Burger.Id);
                return RedirectToPage("./Error");
            }

            return RedirectToPage("./Index");
        }
    }
}
