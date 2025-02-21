using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;
using WebAppAss.Models;

namespace WebAppAss.Pages
{
    public class MenuModel : PageModel
    {
        private readonly WebAppAssContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MenuModel(WebAppAssContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<MenuItem> SearchResults { get; set; } = new List<MenuItem>();

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ItemCategory { get; set; }

        public SelectList? Categories { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            IQueryable<string> categoryQuery = _context.Burgers.Select(b => "Burgers")
                .Union(_context.Drinks.Select(d => "Drinks"))
                .Union(_context.Sides.Select(s => "Sides"))
                .Union(_context.Desserts.Select(d => "Desserts"))
                .Distinct();

            var menuItems = await GetAllMenuItems();

            if (!string.IsNullOrEmpty(SearchString))
            {
                menuItems = menuItems.Where(m => m.Name.Contains(SearchString)).ToList();
            }

            if (!string.IsNullOrEmpty(ItemCategory))
            {
                switch (ItemCategory)
                {
                    case "Burgers":
                        menuItems = menuItems.Where(m => m is Burger).ToList();
                        break;
                    case "Drinks":
                        menuItems = menuItems.Where(m => m is Drink).ToList();
                        break;
                    case "Sides":
                        menuItems = menuItems.Where(m => m is Side).ToList();
                        break;
                    case "Desserts":
                        menuItems = menuItems.Where(m => m is Dessert).ToList();
                        break;
                }
            }

            Categories = new SelectList(await categoryQuery.ToListAsync());
            SearchResults = menuItems;

            return Page();
        }

        public async Task<IActionResult> OnPostBuyAsync(int itemId, int quantity, string itemType)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Account/Login");

            var customer = await _context.CheckoutCustomers
                .FirstOrDefaultAsync(c => c.Email == user.Email);
            if (customer == null || !customer.BasketID.HasValue) return RedirectToPage();

            var basketItem = await _context.BasketItems
                .FirstOrDefaultAsync(bi => bi.BasketID == customer.BasketID && bi.ItemID == itemId && bi.ItemType == itemType);

            if (basketItem == null)
            {
                _context.BasketItems.Add(new BasketItem
                {
                    BasketID = customer.BasketID.Value,
                    ItemID = itemId,
                    ItemType = itemType,
                    Quantity = quantity
                });
            }
            else
            {
                basketItem.Quantity += quantity;
            }
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        private async Task<List<MenuItem>> GetAllMenuItems()
        {
            var burgers = await _context.Burgers.AsNoTracking().ToListAsync();
            var drinks = await _context.Drinks.AsNoTracking().ToListAsync();
            var sides = await _context.Sides.AsNoTracking().ToListAsync();
            var desserts = await _context.Desserts.AsNoTracking().ToListAsync();

            return burgers.Cast<MenuItem>()
                .Concat(drinks)
                .Concat(sides)
                .Concat(desserts)
                .ToList();
        }
    }
}
