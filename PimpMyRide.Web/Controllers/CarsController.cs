namespace PimpMyRide.Web.Controllers
{
    using System.Threading.Tasks;
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PimpMyRide.Services.Contracts;
    using PimpMyRide.Services.Models;

    [Authorize]
    public class CarsController : Controller
    {
        private readonly ICarService carService;

        public CarsController(ICarService carService)
        {
            this.carService = carService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(CarServiceModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var picture = model.Picture == null
                ? null
                : Task.Run(async () => await model.Picture.ToByteArrayAsync()).Result;

            var user = this.carService.GetOwner(this.User.Identity.Name);

            var carId =
                this.carService.Add(user
                , model.Make,
                model.CarModel,
                model.YearOfProduction,
                model.EngineType,
                model.BodyType,
                model.TotalPrice,
                model.Description,
                picture);

            return this.RedirectToAction("Details", "Cars", new { Id = carId });
        }

        public IActionResult Edit(int id)
        {
            if (!this.carService.Exists(id))
            {
                return this.NotFound();
            }

            if (!this.User.IsInRole(WebConstants.AdministratorRole))
            {
                var isOwner = this.carService.IsOwner(id, this.User.Identity.Name);

                if (!isOwner)
                {
                    return this.NotFound();
                }
            }

            var car = this.carService.GetCar(id);

            return this.View(car);
        }

        [HttpPost]
        public IActionResult Edit(int id, CarServiceModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (!this.User.IsInRole(WebConstants.AdministratorRole))
            {
                var isOwner = this.carService.IsOwner(id, this.User.Identity.Name);

                if (!isOwner)
                {
                    return this.NotFound();
                }
            }

            var picture = model.Picture == null
                 ? null
                 : Task.Run(async () => await model.Picture.ToByteArrayAsync()).Result;

            var carId = this.carService.Edit(
                  id,
                  model.Make,
                  model.CarModel,
                  model.YearOfProduction,
                  model.EngineType,
                  model.BodyType,
                  model.TotalPrice,
                  model.Description,
                  picture);


            return this.RedirectToAction("Details", "Cars", new { Id = carId });
        }

        [AllowAnonymous]
        [ViewCounFilter]
        public IActionResult Details(int id)
        {
            if (!this.carService.Exists(id))
            {
                return this.NotFound();
            }

            if (!this.carService.IsActive(id) &&
                (!this.User.IsInRole(WebConstants.AdministratorRole) &&
                !this.User.IsInRole(WebConstants.ModeratorRole) &&
                !this.carService.IsOwner(id, this.User.Identity.Name)))
            {
                return this.NotFound();
            }

            var car = this.carService.GetCarDetails(id);

            return this.View(car);
        }

        public IActionResult Delete(int id)
        {
            if (!this.User.IsInRole(WebConstants.AdministratorRole))
            {
                var isOwner = this.carService.IsOwner(id, this.User.Identity.Name);

                if (!isOwner)
                {
                    return this.NotFound();
                }
            }

            this.carService.Delete(id);

            // should redirect to my cars view

            return this.RedirectToAction("All");
        }

        [AllowAnonymous]
        public IActionResult All(string make, string model, int? from, int? to)
        {
            if (make == null && model == null && from == null && to == null)
            {
                var cars = this.carService.GetAll();

                return this.View(cars);
            }

            var carsSearched = this.carService.GetAllSearch(make, model, from, to);

            return this.View(carsSearched);
        }
    }
}
