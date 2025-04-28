using Microsoft.AspNetCore.Identity;
using RS.Core.Application.Enums;
using RS.Infrastructure.Identity.Entities;

namespace RS.Infrastructure.Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser defaultUser = new();
            defaultUser.UserName = "basicuser";
            defaultUser.Email = "basicuser@gmail.com";
            defaultUser.FirstName = "John";
            defaultUser.LastName = "Duran";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
            }
        }
    }
}
