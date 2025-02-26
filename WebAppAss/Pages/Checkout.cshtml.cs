using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using WebAppAss.Data;
using WebAppAss.Models;

namespace WebAppAss.Pages
{
    [Authorize]
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

        // Retrieves and displays the user's basket items on page load
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
                            item = new CheckoutItem
                            {
                                ID = b.ItemID,
                                Item_Name = burger.Name,
                                Price = burger.Price,
                                Quantity = b.Quantity,
                                CompositeID = $"Burger_{b.ItemID}"
                            };
                        break;
                    case "Drink":
                        var drink = await _db.Drinks.FirstOrDefaultAsync(x => x.Id == b.ItemID);
                        if (drink != null)
                            item = new CheckoutItem
                            {
                                ID = b.ItemID,
                                Item_Name = drink.Name,
                                Price = drink.Price,
                                Quantity = b.Quantity,
                                CompositeID = $"Drink_{b.ItemID}"
                            };
                        break;
                    case "Side":
                        var side = await _db.Sides.FirstOrDefaultAsync(x => x.Id == b.ItemID);
                        if (side != null)
                            item = new CheckoutItem
                            {
                                ID = b.ItemID,
                                Item_Name = side.Name,
                                Price = side.Price,
                                Quantity = b.Quantity,
                                CompositeID = $"Side_{b.ItemID}"
                            };
                        break;
                    case "Dessert":
                        var dessert = await _db.Desserts.FirstOrDefaultAsync(x => x.Id == b.ItemID);
                        if (dessert != null)
                            item = new CheckoutItem
                            {
                                ID = b.ItemID,
                                Item_Name = dessert.Name,
                                Price = dessert.Price,
                                Quantity = b.Quantity,
                                CompositeID = $"Dessert_{b.ItemID}"
                            };
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

        // Updates the quantities of items in the basket based on form input
        public async Task<IActionResult> OnPostUpdateAsync(IFormCollection form)
        {
            var user = await _userManager.GetUserAsync(User);
            var customer = await _db.CheckoutCustomers.FirstOrDefaultAsync(c => c.Email == user.Email);
            if (customer == null || !customer.BasketID.HasValue)
                return RedirectToPage("/Menu");

            var basketItems = await _db.BasketItems
                .Where(b => b.BasketID == customer.BasketID)
                .ToListAsync();

            foreach (var basketItem in basketItems)
            {
                // Construct the composite key:
                string compositeKey = $"{basketItem.ItemType}_{basketItem.ItemID}";
                if (form.TryGetValue($"quantity[{compositeKey}]", out StringValues quantityValue) &&
                    int.TryParse(quantityValue.FirstOrDefault(), out int newQuantity))
                {
                    if (newQuantity >= 1 && newQuantity <= 20)
                    {
                        basketItem.Quantity = newQuantity;
                    }
                }
            }

            await _db.SaveChangesAsync();
            return RedirectToPage();
        }

        // Removes a specific item from the basket based on its composite identifier
        public async Task<IActionResult> OnPostRemoveAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Item identifier is required.");

            // Expecting format "ItemType_ItemID", e.g., "Burger_7"
            var parts = id.Split('_');
            if (parts.Length != 2)
                return BadRequest("Invalid item identifier.");

            var itemType = parts[0];
            if (!int.TryParse(parts[1], out int itemId))
                return BadRequest("Invalid item id.");

            var user = await _userManager.GetUserAsync(User);
            var customer = await _db.CheckoutCustomers.FirstOrDefaultAsync(c => c.Email == user.Email);
            if (customer == null || !customer.BasketID.HasValue)
                return RedirectToPage("/Menu");

            var basketItem = await _db.BasketItems
                .FirstOrDefaultAsync(b => b.BasketID == customer.BasketID && b.ItemID == itemId && b.ItemType == itemType);
            if (basketItem != null)
            {
                _db.BasketItems.Remove(basketItem);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        // Processes the purchase by creating an order and clearing the basket
        public async Task<IActionResult> OnPostBuyAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var order = new OrderHistory
            {
                Email = user.Email,
            };

            _db.OrderHistories.Add(order);
            await _db.SaveChangesAsync();

            int newOrderNo = order.OrderNo;

            CheckoutCustomer customer = await _db.CheckoutCustomers.FindAsync(user.Email);
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

            await _db.SaveChangesAsync();

            return RedirectToPage("/PurchaseConfirmation", new { orderNumber = newOrderNo });
        }
    }
}
