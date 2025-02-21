using Microsoft.AspNetCore.Identity;

namespace WebAppAss.Data
{
    public class IdentitySeedData
    {
        public static async Task Initialize(WebAppAssContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            string adminRole = "Admin";
            string memberRole = "Member";
            string password4all = "Pa$$w0rd";

            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            if (await roleManager.FindByNameAsync(@memberRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(memberRole));
            }

            if (await userManager.FindByNameAsync("admin@pm.me") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin@pm.me",
                    Email = "admin@pm.me",
                    PhoneNumber = "03941884137"
                };

                var result = await userManager.CreateAsync(user, password4all);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, adminRole);
                }
            }

            if (await userManager.FindByNameAsync("member@pm.me") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "member@pm.me",
                    Email = "member@pm.me",
                    PhoneNumber = "04181416537"
                };

                var result = await userManager.CreateAsync(user, password4all);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }
        }
    }
}
