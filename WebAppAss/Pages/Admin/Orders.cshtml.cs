using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;
using WebAppAss.Models;

namespace WebAppAss.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class OrdersModel : PageModel
    {
        private readonly WebAppAssContext _db;

        public OrdersModel(WebAppAssContext db)
        {
            _db = db;
        }

        public List<AdminOrderView> Orders { get; set; }

        public async Task OnGetAsync()
        {
            // Retrieve all order histories
            var orderHistories = await _db.OrderHistories.ToListAsync();
            Orders = new List<AdminOrderView>();

            foreach (var order in orderHistories)
            {
                // Retrieve items for the order
                var orderItems = await _db.OrderItems
                    .Where(oi => oi.OrderNo == order.OrderNo)
                    .ToListAsync();

                var itemsViewModel = new List<OrderItemView>();

                // Loop through each order item and determine the actual menu item name
                foreach (var orderItem in orderItems)
                {
                    string itemName = string.Empty;

                    switch (orderItem.ItemType)
                    {
                        case "Burger":
                            var burger = await _db.Burgers.FirstOrDefaultAsync(b => b.Id == orderItem.ItemID);
                            if (burger != null)
                                itemName = burger.Name;
                            break;
                        case "Drink":
                            var drink = await _db.Drinks.FirstOrDefaultAsync(d => d.Id == orderItem.ItemID);
                            if (drink != null)
                                itemName = drink.Name;
                            break;
                        case "Side":
                            var side = await _db.Sides.FirstOrDefaultAsync(s => s.Id == orderItem.ItemID);
                            if (side != null)
                                itemName = side.Name;
                            break;
                        case "Dessert":
                            var dessert = await _db.Desserts.FirstOrDefaultAsync(d => d.Id == orderItem.ItemID);
                            if (dessert != null)
                                itemName = dessert.Name;
                            break;
                        default:
                            itemName = "Unknown Item";
                            break;
                    }

                    itemsViewModel.Add(new OrderItemView
                    {
                        ItemID = orderItem.ItemID,
                        ItemType = orderItem.ItemType,
                        ItemName = itemName,
                        Quantity = orderItem.Quantity
                    });
                }

                Orders.Add(new AdminOrderView
                {
                    OrderNo = order.OrderNo,
                    Email = order.Email,
                    Items = itemsViewModel
                });
            }
        }
    }
}
