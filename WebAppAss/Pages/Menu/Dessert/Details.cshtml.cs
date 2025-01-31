using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;

namespace WebAppAss.Pages.Menu.Dessert
{
    public class DetailsModel : PageModel
    {
        private readonly WebAppAss.Data.WebAppAssContext _context;

        public DetailsModel(WebAppAss.Data.WebAppAssContext context)
        {
            _context = context;
        }

      public WebAppAss.Models.Dessert Dessert { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Desserts == null)
            {
                return NotFound();
            }

            var dessert = await _context.Desserts.FirstOrDefaultAsync(m => m.Id == id);
            if (dessert == null)
            {
                return NotFound();
            }
            else 
            {
                Dessert = dessert;
            }
            return Page();
        }
    }
}
