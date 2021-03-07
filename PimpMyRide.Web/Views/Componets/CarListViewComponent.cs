namespace PimpMyRide.Web.Views.Componets
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using PimpMyRide.Services.Models;

    public class CarListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<CarListServiceModel> cars)
        {
            return this.View(cars);
        }
    }
}
