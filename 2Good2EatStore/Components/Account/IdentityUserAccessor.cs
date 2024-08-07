using _2Good2EatStore.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace _2Good2EatStore.Components.Account
{
    internal sealed class IdentityUserAccessor(UserManager<ApplicationUser> userManager, IdentityRedirectManager redirectManager)
    {
        public async Task<ApplicationUser> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            }

            return user;
        }

        public async Task AddAdminRole(ApplicationUser user)
        {
            await userManager.AddToRoleAsync(user, "Admin");

        }
    }
}
