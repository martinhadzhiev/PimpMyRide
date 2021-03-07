namespace PimpMyRide.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using PimpMyRide.Services.Contracts;
    using PimpMyRide.Services.Models;

    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public IActionResult List()
        {
            var ordersJson = this.HttpContext.Session.GetString(WebConstants.SessionOrderKey);

            var orders = ordersJson == null
                ? new Dictionary<string, Order>()
                : this.DeserializeFromJson<Dictionary<string, Order>>(ordersJson);

            var ordersView = orders.Select(o => new OrderListServiceModel
            {
                Id = o.Key,
                Price = o.Value.Parts.Sum(p => p.Price),
                Seller = $"{o.Value.Seller.FirstName} {o.Value.Seller.LastName}",
                Parts = string.Join(", ", o.Value.Parts.Select(p => p.Name))
            });

            return this.View(ordersView);
        }

        public IActionResult Add(int partId, int carId)
        {
            string username = this.User.Identity.Name;

            if (!this.orderService.Exists(partId, username))
            {
                return this.NotFound();
            }

            if (this.orderService.IsOrdered(partId, username))
            {
                this.TempData.AddErrorMessage("You have already requested this part.");

                return this.RedirectToAction("Details", "Cars", new { id = carId });
            }

            var part = this.orderService.Get(partId);

            this.AddOrCreateOrder(part);

            this.TempData.AddSuccessMessage("Part added to your orders.");

            return this.RedirectToAction("Details", "Cars", new { id = carId });
        }

        public IActionResult Delete(string orderId)
        {
            if (!string.IsNullOrEmpty(orderId))
            {
                var ordersJson = this.HttpContext.Session.GetString(WebConstants.SessionOrderKey);

                var orders = this.DeserializeFromJson<Dictionary<string, Order>>(ordersJson);

                if (orders.ContainsKey(orderId))
                {
                    orders.Remove(orderId);

                    this.HttpContext.Session
                        .SetString(WebConstants.SessionOrderKey, this.SerializeToJson(orders));
                }
            }

            return this.RedirectToAction("List");
        }

        public IActionResult SendRequest(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                return this.NotFound();
            }

            var ordersJson = this.HttpContext.Session.GetString(WebConstants.SessionOrderKey);
            var orders = this.DeserializeFromJson<Dictionary<string, Order>>(ordersJson);

            if (orders.ContainsKey(orderId))
            {
                var order = orders[orderId];
                var partIds = order.Parts.Select(p => p.Id);

                this.orderService.AddOrder(order.Seller.Id, order.Buyer.Id, partIds);

                orders.Remove(orderId);

                this.HttpContext.Session
                    .SetString(WebConstants.SessionOrderKey, this.SerializeToJson(orders));
            }

            return this.RedirectToAction("List");
        }

        public IActionResult ReviewRequests()
        {
            // not implemented

            return this.View();
        }

        public IActionResult MyOrders()
        {
            var orders = this.orderService.GetOrders(this.User.Identity.Name);

            return this.View(orders);
        }

        public IActionResult Cancel(int id)
        {
            this.orderService.CancelOrder(id, this.User.Identity.Name);

            this.TempData.AddSuccessMessage("Order cancelled.");

            return this.RedirectToAction("MyOrders");
        }

        #region HELPERS

        private void AddOrCreateOrder(Part part)
        {
            var ordersJson = this.HttpContext.Session.GetString(WebConstants.SessionOrderKey);
            var seller = this.orderService.GetUser(part.CarId);
            var buyer = this.orderService.GetUser(this.User.Identity.Name);

            var orders = ordersJson == null
                ? new Dictionary<string, Order>()
                : this.DeserializeFromJson<Dictionary<string, Order>>(ordersJson);

            var sellerName = seller.UserName;

            if (!orders.ContainsKey(sellerName))
            {
                orders.Add(sellerName, new Order()
                {
                    Buyer = buyer,
                    Seller = seller
                });
            }

            if (orders[sellerName].Parts.All(p => p.Id != part.Id))
            {
                orders[sellerName].Parts.Add(part);
            }

            this.HttpContext.Session.SetString(WebConstants.SessionOrderKey, this.SerializeToJson(orders));
        }

        private string SerializeToJson(object obj)
            => JsonConvert.SerializeObject(obj);

        private T DeserializeFromJson<T>(string jsonString)
            => string.IsNullOrEmpty(jsonString) ? default(T)
            : JsonConvert.DeserializeObject<T>(jsonString);

        #endregion
    }
}
