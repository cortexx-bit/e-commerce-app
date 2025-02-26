using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;

namespace WebAppAss.Pages.Menu.Dessert
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly WebAppAss.Data.WebAppAssContext _context;

        public IndexModel(WebAppAss.Data.WebAppAssContext context)
        {
            _context = context;
        }

        public IList<WebAppAss.Models.Dessert> Dessert { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Desserts != null)
            {
                Dessert = await _context.Desserts.ToListAsync();
            }
        }
    }
}
