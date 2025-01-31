using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppAss.Data;

namespace WebAppAss.Pages.Menu.Burger
{
    public static class BurgerType
    {
        public static SelectList GetBurgerTypeList(object selectedValue = null)
        {
            var types = new List<string>
            {
                "Beef", "Chicken", "Fish", "Lamb", "Vegan"
            };
            return new SelectList(types, selectedValue);
        }
    }
}
