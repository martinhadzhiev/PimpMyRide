namespace PimpMyRide.Services.Admin.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Extensions.Internal;
    using Models;

    public class AdminUsersService : IAdminUsersService
    {
        private readonly PimpMyRideDbContext dbContext;
        private readonly UserManager<User> userManager;

        public AdminUsersService(PimpMyRideDbContext dbContext, UserManager<User> manager)
        {
            this.dbContext = dbContext;
            this.userManager = manager;
        }

        public IEnumerable<UserListModel> GetUsers()
        {
            IList<UserListModel> users = new List<UserListModel>();

            this.dbContext.Users.AsAsyncEnumerable()
                .ForEach(u => users.Add(new UserListModel()
                {
                    Id = u.Id,
                    Username = u.UserName,
                    FullName = $"{u.FirstName} {u.LastName}",
                    Roles = this.userManager.GetRoles(u)
                }));

            return users;
        }

        public void AddToRole(string userId, string roleName)
        {
            var user = Task.Run(async () => await this.userManager.FindByIdAsync(userId)).Result;

            if (user != null)
            {
                Task.Run(async () => await this.userManager
                .AddToRoleAsync(user, roleName))
                .GetAwaiter()
                .GetResult();
            }
        }

        public void RemoveFromRole(string userId, string roleName)
        {
            var user = Task.Run(async () => await this.userManager.FindByIdAsync(userId)).Result;

            if (user != null)
            {
                Task.Run(async () => await this.userManager
                        .RemoveFromRoleAsync(user, roleName))
                    .GetAwaiter()
                    .GetResult();
            }
        }

        public void DeleteUser(string userId)
        {
            var user = Task.Run(async () => await this.userManager.FindByIdAsync(userId)).Result;

            if (user != null)
            {
                Task.Run(async () => await this.userManager
                    .DeleteAsync(user))
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}
