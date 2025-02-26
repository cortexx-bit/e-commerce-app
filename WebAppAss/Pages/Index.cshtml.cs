using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;
using WebAppAss.Models;

namespace WebAppAss.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WebAppAssContext _context;
        public List<MenuItem> SpecialItems { get; set; } = new List<MenuItem>();

        public IndexModel(WebAppAssContext context)
        {
            _context = context;
        }
        // Loads special menu items for display on the home page
        public async Task OnGetAsync()
        {
            var burgers = await _context.Burgers.Where(b => b.Special).ToListAsync();
            SpecialItems = burgers.Cast<MenuItem>().ToList();
        }
    }
}
