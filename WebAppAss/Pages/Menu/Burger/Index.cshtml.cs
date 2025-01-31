using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;
using WebAppAss.Models;

namespace WebAppAss.Pages.Menu.Burger
{
    public class IndexModel : PageModel
    {
        private readonly WebAppAss.Data.WebAppAssContext _context;

        public IndexModel(WebAppAss.Data.WebAppAssContext context)
        {
            _context = context;
        }

        public IList<WebAppAss.Models.Burger> Burger { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Burgers != null)
            {
                Burger = await _context.Burgers.ToListAsync();
            }
        }
    }
}
