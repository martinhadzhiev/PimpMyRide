namespace PimpMyRide.Web.Views.Componets
{
    using Microsoft.AspNetCore.Mvc;

    public class OrderListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return this.View();
        }
    }
}
