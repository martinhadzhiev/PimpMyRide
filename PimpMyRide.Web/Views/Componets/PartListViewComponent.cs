namespace PimpMyRide.Web.Views.Componets
{
    using Microsoft.AspNetCore.Mvc;
    using PimpMyRide.Services.Contracts;

    public class PartListViewComponent : ViewComponent
    {
        private readonly IPartService partService;

        public PartListViewComponent(IPartService partService)
        {
            this.partService = partService;
        }

        public IViewComponentResult Invoke(int id)
        {
            var parts = this.partService.GetParts(id);

            return this.View(parts);
        }
    }
}
