using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;
using WebAppAss.Models;

namespace WebAppAss.Pages.Menu.Burger
{
    public class EditModel : PageModel
    {
        private readonly WebAppAss.Data.WebAppAssContext _context;

        public EditModel(WebAppAss.Data.WebAppAssContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WebAppAss.Models.Burger Burger { get; set; } = default!;
        public SelectList TypeSL { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Burgers == null)
            {
                return NotFound();
            }
            Burger = await _context.Burgers
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Burger == null)
            {
                return NotFound();
            }
            TypeSL = BurgerType.GetBurgerTypeList(Burger.Type);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid) return Page();

            var burgerToUpdate = await _context.Burgers.FindAsync(id);
            if (burgerToUpdate == null)
            {
                return NotFound();
            }
            foreach (var file in Request.Form.Files)
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                Burger.ImageData = ms.ToArray();
                ms.Close();
                ms.Dispose();
            }
            // Only update these specific fields
            if (await TryUpdateModelAsync<Models.Burger>(
                burgerToUpdate,
                "Burger",
                b => b.Name,
                b => b.Description,
                b => b.Price,
                b => b.Type,
                b => b.IsVegetarian,
                b => b.IsAvailable,
                b => b.Special,
                b => b.ImageDescription))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Another user has modified this record. Please review the changes and try again.");
                }
            }
            TypeSL = BurgerType.GetBurgerTypeList(Burger.Type);
            return Page();
        }
    }
}
