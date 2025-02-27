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
        private readonly IBasketService _basketService;

        public MenuModel(WebAppAssContext context, UserManager<IdentityUser> userManager, IBasketService basketService)
        {
            _context = context;
            _userManager = userManager;
            _basketService = basketService;
        }

        public IList<MenuItem> SearchResults { get; set; } = new List<MenuItem>();

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ItemCategory { get; set; }

        public SelectList? Categories { get; set; }

        // Retrieves and filters menu items based on search criteria for display on the menu page
        public async Task<IActionResult> OnGetAsync()
        {
            IQueryable<string> categoryQuery = _context.Burgers
                .Where(b => b.IsAvailable) 
                .Select(b => "Burgers")
                .Union(_context.Drinks.Where(d => d.IsAvailable) 
                    .Select(d => "Drinks"))
                .Union(_context.Sides.Where(s => s.IsAvailable) 
                    .Select(s => "Sides"))
                .Union(_context.Desserts.Where(d => d.IsAvailable) 
                    .Select(d => "Desserts"))
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

        // Handles adding a menu item to the user's basket
        public async Task<IActionResult> OnPostBuyAsync(int itemId, int quantity, string itemType)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new {area = "Identity"});
            }

            var customer = await _context.CheckoutCustomers
                .FirstOrDefaultAsync(c => c.Email == user.Email);
            if (customer == null || !customer.BasketID.HasValue) return RedirectToPage();

            MenuItem item = await GetMenuItemByIdAndType(itemId, itemType);
            if (item == null || !item.IsAvailable)
            {
                ModelState.AddModelError("", "This item is currently out of stock and cannot be added to your basket.");
                return Page();
            }

            await _basketService.AddItemToBasketAsync(user, itemId, quantity, itemType);
            return RedirectToPage();
        }

        // Retrieves all menu items from the database for filtering
        private async Task<List<MenuItem>> GetAllMenuItems()
        {
            var burgers = await _context.Burgers.AsNoTracking().Where(b => b.IsAvailable).ToListAsync();
            var drinks = await _context.Drinks.AsNoTracking().Where(d => d.IsAvailable).ToListAsync(); 
            var sides = await _context.Sides.AsNoTracking().Where(s => s.IsAvailable).ToListAsync(); 
            var desserts = await _context.Desserts.AsNoTracking().Where(d => d.IsAvailable).ToListAsync();

            // Combine all menu items into a single list
            return burgers.Cast<MenuItem>()
                .Concat(drinks)
                .Concat(sides)
                .Concat(desserts)
                .ToList();
        }
        private async Task<MenuItem> GetMenuItemByIdAndType(int itemId, string itemType)
        {
            return itemType switch
            {
                "Burger" => await _context.Burgers.FirstOrDefaultAsync(b => b.Id == itemId),
                "Drink" => await _context.Drinks.FirstOrDefaultAsync(d => d.Id == itemId),
                "Side" => await _context.Sides.FirstOrDefaultAsync(s => s.Id == itemId),
                "Dessert" => await _context.Desserts.FirstOrDefaultAsync(d => d.Id == itemId),
                _ => null
            };
        }
    }
}
