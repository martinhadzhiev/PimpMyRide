namespace PimpMyRide.Web.Areas.Moderator.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PimpMyRide.Services.Moderator.Contracts;

    public class CarsController : BaseModeratorController
    {
        private readonly IModeratorCarsService carsService;

        public CarsController(IModeratorCarsService carsService)
        {
            this.carsService = carsService;
        }

        public IActionResult List()
        {
            var inActiveCars = this.carsService.GetAllInActive();

            return this.View(inActiveCars);
        }

        public IActionResult Details(int id)
        {
            var car = this.carsService.GetDetails(id);

            return this.View(car);
        }

        public IActionResult Approve(int id)
        {
            this.carsService.Approve(id);

            this.TempData[WebConstants.TempDataSuccessMessageKey] = "Car has been succsessfully aproved.";

            return this.RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            this.carsService.Delete(id);

            this.TempData[WebConstants.TempDataErrorMessageKey] = "Car has been deleted.";

            return this.RedirectToAction("List");
        }
    }
}
