using Microsoft.AspNetCore.Identity;

public interface IBasketService
    // To allow items to be added from Index, Menu and separate items pages without repeating code
{
    Task AddItemToBasketAsync(IdentityUser user, int itemId, int quantity, string itemType);
}
