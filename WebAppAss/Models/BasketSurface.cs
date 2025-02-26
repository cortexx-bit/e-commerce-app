using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppAss.Data;
using WebAppAss.Models;

// Implementation of basket logic
public class BasketService : IBasketService
{
    private readonly WebAppAssContext _context;

    public BasketService(WebAppAssContext context)
    {
        _context = context;
    }

    public async Task AddItemToBasketAsync(IdentityUser user, int itemId, int quantity, string itemType)
    {
        var customer = await _context.CheckoutCustomers.FirstOrDefaultAsync(c => c.Email == user.Email);
        if (customer == null || !customer.BasketID.HasValue)
        {
            throw new InvalidOperationException("No basket found for this customer.");
        }

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
    }
}
