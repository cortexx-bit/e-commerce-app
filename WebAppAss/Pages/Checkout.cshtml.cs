using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;
using WebAppAss.Models;

namespace WebAppAss.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly WebAppAssContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public OrderHistory Order = new OrderHistory();

        public IList<CheckoutItem> Items { get; private set; } = new List<CheckoutItem>();
        public decimal Total { get; private set; }

        public CheckoutModel(WebAppAssContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Account/Login");

            var customer = await _db.CheckoutCustomers.FirstOrDefaultAsync(c => c.Email == user.Email);
            if (customer == null || !customer.BasketID.HasValue) return RedirectToPage("/Menu");

            var basketItems = await _db.BasketItems
            .Where(b => b.BasketID == customer.BasketID)
            .ToListAsync();

            Items = new List<CheckoutItem>();
            foreach (var b in basketItems)
            {
                CheckoutItem item = null;
                switch (b.ItemType)
                {
                    case "Burger":
                        var burger = await _db.Burgers.FirstOrDefaultAsync(x => x.Id == b.ItemID);
                        if (burger != null)
                            item = new CheckoutItem { ID = b.ItemID, Item_Name = burger.Name, Price = burger.Price, Quantity = b.Quantity };
                        break;
                    case "Drink":
                        var drink = await _db.Drinks.FirstOrDefaultAsync(x => x.Id == b.ItemID);
                        if (drink != null)
                            item = new CheckoutItem { ID = b.ItemID, Item_Name = drink.Name, Price = drink.Price, Quantity = b.Quantity };
                        break;
                    case "Side":
                        var side = await _db.Sides.FirstOrDefaultAsync(x => x.Id == b.ItemID);
                        if (side != null)
                            item = new CheckoutItem { ID = b.ItemID, Item_Name = side.Name, Price = side.Price, Quantity = b.Quantity };
                        break;
                    case "Dessert":
                        var dessert = await _db.Desserts.FirstOrDefaultAsync(x => x.Id == b.ItemID);
                        if (dessert != null)
                            item = new CheckoutItem { ID = b.ItemID, Item_Name = dessert.Name, Price = dessert.Price, Quantity = b.Quantity };
                        break;
                }
                if (item != null)
                {
                    Items.Add(item);
                }
            }

            Total = Items.Sum(i => i.Quantity * i.Price);
            return Page();
        }
        public async Task<IActionResult> OnPostBuyAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var order = new OrderHistory
            {
                Email = user.Email,
            };

            // Let SQL Server auto-generate OrderNo
            _db.OrderHistories.Add(order);
            await _db.SaveChangesAsync(); // Save first to get auto-generated OrderNo

            int newOrderNo = order.OrderNo; // OrderNo is now auto-generated


            CheckoutCustomer customer = await _db
                .CheckoutCustomers
                .FindAsync(user.Email);
            var basketItems = _db.BasketItems.Where(b => b.BasketID == customer.BasketID).ToList();


            foreach (var item in basketItems)
            {
                OrderItem oi = new OrderItem
                {
                    OrderNo = newOrderNo,
                    ItemID = item.ItemID,
                    ItemType = item.ItemType,
                    Quantity = item.Quantity,
                };
                _db.OrderItems.Add(oi);
                _db.BasketItems.Remove(item);
            }
            _db.BasketItems.RemoveRange(basketItems);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
