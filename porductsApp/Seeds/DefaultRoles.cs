using Microsoft.AspNetCore.Identity;

namespace porductsApp.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("Seller"));
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }
        }
    }
}
