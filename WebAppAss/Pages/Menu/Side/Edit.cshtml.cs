using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;
using WebAppAss.Models;
using WebAppAss.Pages.Menu.Burger;

namespace WebAppAss.Pages.Menu.Side
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly WebAppAss.Data.WebAppAssContext _context;

        public EditModel(WebAppAss.Data.WebAppAssContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WebAppAss.Models.Side Side { get; set; } = default!;

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var sideToUpdate = await _context.Sides.FindAsync(Side.Id);
            if (sideToUpdate == null)
            {
                return NotFound();
            }
            foreach (var file in Request.Form.Files)
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                Side.ImageData = ms.ToArray();
                ms.Close();
                ms.Dispose();
            }
            if (await TryUpdateModelAsync<Models.Side>(
                sideToUpdate,
                "Side",
                s => s.Name,
                s => s.Description,
                s => s.Price,
                s => s.Size,
                s => s.IsAvailable,
                s => s.ImageDescription))
            {
                try
                {
                    sideToUpdate.Slug = sideToUpdate.GenerateSlug();
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Another user has modified this record. Please review the changes and try again.");
                }
            }
            return Page();
        }
    }
}
