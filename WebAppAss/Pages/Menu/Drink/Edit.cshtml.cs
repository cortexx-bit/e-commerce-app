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

namespace WebAppAss.Pages.Menu.Drink
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
        public WebAppAss.Models.Drink Drink { get; set; } = default!;

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var drinkToUpdate = await _context.Drinks.FindAsync(Drink.Id);
            if (drinkToUpdate == null) return NotFound();
            foreach (var file in Request.Form.Files)
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                Drink.ImageData = ms.ToArray();
                ms.Close();
                ms.Dispose();
            }
            if (await TryUpdateModelAsync<Models.Drink>(
                drinkToUpdate,
                "Drink",
                d => d.Name,
                d => d.Description,
                d => d.Price,
                d => d.Size,
                d => d.IsAlcoholic,
                d => d.IsAvailable,
                d => d.ImageDescription))
            {
                try
                {
                    drinkToUpdate.Slug = drinkToUpdate.GenerateSlug();
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
