namespace PimpMyRide.Services.Admin.Contracts
{
    using System.Collections.Generic;
    using Models;

    public interface IAdminUsersService
    {
        IEnumerable<UserListModel> GetUsers();

        void AddToRole(string userId, string roleName);

        void RemoveFromRole(string userId, string roleName);

        void DeleteUser(string userId);
    }
}