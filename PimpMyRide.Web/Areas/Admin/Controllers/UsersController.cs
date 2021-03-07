namespace PimpMyRide.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PimpMyRide.Services.Admin.Contracts;

    public class UsersController : BaseAdminController
    {
        private readonly IAdminUsersService usersService;

        public UsersController(IAdminUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult List()
        {
            var users = this.usersService.GetUsers();

            return this.View(users);
        }

        public IActionResult AddToRoleModerator(string id)
        {
            if (id != null)
            {
                this.usersService.AddToRole(id, WebConstants.ModeratorRole);
            }

            return this.RedirectToAction(nameof(this.List));
        }

        public IActionResult RemoveFromRoleModerator(string id)
        {
            if (id != null)
            {
                this.usersService.RemoveFromRole(id, WebConstants.ModeratorRole);
            }

            return this.RedirectToAction(nameof(this.List));
        }

        public IActionResult Delete(string id)
        {
            if (id != null)
            {
                this.usersService.DeleteUser(id);
            }

            return this.RedirectToAction(nameof(this.List));
        }
    }
}
