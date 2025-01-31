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
    public class DetailsModel : PageModel
    {
        private readonly WebAppAss.Data.WebAppAssContext _context;

        public DetailsModel(WebAppAss.Data.WebAppAssContext context)
        {
            _context = context;
        }

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
    }
}
