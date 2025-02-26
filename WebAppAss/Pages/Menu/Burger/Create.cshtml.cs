using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppAss.Data;
using WebAppAss.Models;

namespace WebAppAss.Pages.Menu.Burger
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly WebAppAss.Data.WebAppAssContext _context;

        public CreateModel(WebAppAss.Data.WebAppAssContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WebAppAss.Models.Burger Burger { get; set; }
        public SelectList TypeOptions { get; set; }
        public IActionResult OnGet()
        {
            TypeOptions = BurgerType.GetBurgerTypeList();
            return Page();
        }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            TypeOptions = BurgerType.GetBurgerTypeList();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            foreach (var file in Request.Form.Files)
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                Burger.ImageData = ms.ToArray();
                ms.Close();
                ms.Dispose();
            }
            _context.Burgers.Add(Burger);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
