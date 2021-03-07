namespace PimpMyRide.Web.Controllers
{
    using System.Threading.Tasks;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PimpMyRide.Services.Contracts;
    using PimpMyRide.Services.Models;

    [Authorize]
    public class PartsController : Controller
    {
        private readonly IPartService partService;

        public PartsController(IPartService partService)
        {
            this.partService = partService;
        }

        public IActionResult Add(int id)
        {
            if (!this.partService.CarExists(id))
            {
                return this.NotFound();
            }

            if (!this.User.IsInRole(WebConstants.AdministratorRole) &&
                !this.partService.IsOwner(id, this.User.Identity.Name))
            {
                return this.NotFound();
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Add(int id, PartCrudServiceModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (!this.partService.CarExists(id))
            {
                return this.NotFound();
            }

            if (!this.User.IsInRole(WebConstants.AdministratorRole) &&
                !this.partService.IsOwner(id, this.User.Identity.Name))
            {
                return this.NotFound();
            }

            var picture = model.Picture == null
                ? null
                : Task.Run(async () => await model.Picture.ToByteArrayAsync()).Result;

            this.partService.AddPart(id, model.Name, model.Price, model.Description, picture);

            return this.RedirectToAction("Details", "Cars", new { id = id });
        }

        public IActionResult Edit(int id)
        {
            if (!this.partService.PartExists(id))
            {
                return this.NotFound();
            }

            var part = this.partService.GetPart(id);

            if (!this.User.IsInRole(WebConstants.AdministratorRole) &&
                !this.partService.IsOwner(part.CarId, this.User.Identity.Name))
            {
                return this.NotFound();
            }

            return this.View(part);
        }

        [HttpPost]
        public IActionResult Edit(int id, PartCrudServiceModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (!this.partService.PartExists(id))
            {
                return this.NotFound();
            }

            var part = this.partService.GetPart(id);

            if (!this.User.IsInRole(WebConstants.AdministratorRole) &&
                !this.partService.IsOwner(part.CarId, this.User.Identity.Name))
            {
                return this.NotFound();
            }

            var picture = model.Picture == null
                ? null
                : Task.Run(async () => await model.Picture.ToByteArrayAsync()).Result;

            this.partService.EditPart(id, model.Name, model.Price, model.Description, picture);

            return this.RedirectToAction("Details", "Cars", new { id = part.CarId });
        }

        public IActionResult Delete(int id)
        {
            if (!this.partService.PartExists(id))
            {
                return this.NotFound();
            }

            var part = this.partService.GetPart(id);

            if (!this.User.IsInRole(WebConstants.AdministratorRole) &&
                !this.partService.IsOwner(part.CarId, this.User.Identity.Name))
            {
                return this.NotFound();
            }

            this.partService.DeletePart(id);

            return this.RedirectToAction("Details", "Cars", new { id = part.CarId });
        }
    }
}
