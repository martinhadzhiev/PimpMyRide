namespace PimpMyRide.Web.Areas.Moderator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = WebConstants.ModeratorOrAdministrator)]
    [Area(WebConstants.ModeratorArea)]
    public class BaseModeratorController : Controller
    {
    }
}
