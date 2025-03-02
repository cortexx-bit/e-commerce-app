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
using WebAppAss.Pages.Menu.Burger;

namespace WebAppAss.Pages.Menu.Dessert
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
        public WebAppAss.Models.Dessert Dessert { get; set; } = default!;

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var dessertToUpdate = await _context.Desserts.FindAsync(Dessert.Id);
            if (dessertToUpdate == null)
            {
                return NotFound();
            }
            foreach (var file in Request.Form.Files)
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                Dessert.ImageData = ms.ToArray();
                ms.Close();
                ms.Dispose();
            }
            // Only update these specific fields
            if (await TryUpdateModelAsync<Models.Dessert>(
                dessertToUpdate,
                "Dessert",
                d => d.Name,
                d => d.Description,
                d => d.Price,
                d => d.IsWarmDessert,
                d => d.IsAvailable,
                d => d.ImageDescription))
            {
                try
                {
                    dessertToUpdate.Slug = dessertToUpdate.GenerateSlug();
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
