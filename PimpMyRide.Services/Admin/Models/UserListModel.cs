namespace PimpMyRide.Services.Admin.Models
{
    using Common;
    using Data.Models;

    public class UserListModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string[] Roles { get; set; }
    }
}
