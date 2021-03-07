namespace PimpMyRide.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using PimpMyRide.Services.Contracts;

    public class ViewCounFilter : ActionFilterAttribute
    {
        private int carId;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                this.carId = (int)context.ActionArguments["id"];
            }
            catch
            {
                return;
            }

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var carService = (ICarService)context.HttpContext.RequestServices.GetService(typeof(ICarService));

            carService.IncreaseViews(this.carId);

            base.OnActionExecuted(context);
        }
    }
}
