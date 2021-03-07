namespace PimpMyRide.Services.Infrastructure.Extensions
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;

    public static class UserManagerExtensions
    {
        public static string[] GetRoles(this UserManager<User> userManager, User user)
             => Task.Run(async () => await userManager.GetRolesAsync(user)).GetAwaiter().GetResult().ToArray();
    }
}
