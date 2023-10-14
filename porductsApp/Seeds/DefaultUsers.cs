using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using porductsApp.Models;

namespace porductsApp.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
        {
            try
            {
                ApplicationUser admin = CreateUser("admin", "ah.mohsen75.com");

                ApplicationUser? user = await userManager.FindByNameAsync(admin.UserName);

                if (user is null)
                {
                    await userManager.CreateAsync(admin, "Ah123456#");
                    await userManager.AddToRoleAsync(admin, "Admin");
                }

                var seller = CreateUser("seller", "seller@gmail.com");

                ApplicationUser? sellerUser = await userManager.FindByNameAsync(seller.UserName);

                if (sellerUser is null)
                {
                    await userManager.CreateAsync(seller, "Ah123456#");
                    await userManager.AddToRoleAsync(seller, "Seller");
                    //await AddUserWithRoles(userManager, seller, "123456", "Seller");
                }

                var customer = CreateUser("customer", "cutomer@gmail.com");

                ApplicationUser customerUser = await userManager.FindByNameAsync(customer.UserName);

                if (customerUser is null)
                {
                    await userManager.CreateAsync(customer, "Ah123456#");
                    await userManager.AddToRoleAsync(customer, "Customer");
                    //await AddUserWithRoles(userManager, customer, "123456", "Customer");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static ApplicationUser CreateUser(string name , string email)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = name,
                Email = email,
                EmailConfirmed = true
            };
            return applicationUser;
        }

        private static async Task AddUserWithRoles(UserManager<ApplicationUser> userManager, ApplicationUser user , string password, string role)
        {
            try
            {
                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, role);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
