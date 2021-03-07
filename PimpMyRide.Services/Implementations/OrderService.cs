namespace PimpMyRide.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class OrderService : IOrderService
    {
        private readonly PimpMyRideDbContext dbContext;

        public OrderService(PimpMyRideDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Exists(int partId, string username)
        {
            if (this.dbContext.Parts.Any(p => p.Id == partId))
            {
                var carId = this.dbContext.Parts.Find(partId).CarId;

                return !this.IsOwner(carId, username);
            }

            return false;
        }

        public Part Get(int id)
            => this.dbContext.Parts.FirstOrDefault(p => p.Id == id);

        public User GetUser(int carId)
            => this.dbContext.Cars
            .Where(c => c.Id == carId)
                .Select(c => c.Owner)
                .FirstOrDefault();

        public User GetUser(string username)
            => this.dbContext.Users
                .FirstOrDefault(u => u.UserName == username);

        public void AddOrder(string sellerId, string buyerId, IEnumerable<int> parts)
        {
            var order = new Order
            {
                BuyerId = buyerId,
                SellerId = sellerId
            };

            foreach (var partId in parts)
            {
                var part = this.dbContext.Parts.Find(partId);

                order.Parts.Add(part);
            }

            order.Price = order.Parts.Sum(p => p.Price);

            this.dbContext.Add(order);
            this.dbContext.SaveChanges();
        }

        public void CancelOrder(int orderId, string username)
        {
            var order = this.dbContext.Orders
                .FirstOrDefault(o => o.Id == orderId && o.Buyer.UserName == username);

            if (order != null)
            {
                this.dbContext.Orders.Remove(order);

                this.dbContext.SaveChanges();
            }
        }

        public IEnumerable<MyOrdersListServiceModel> GetOrders(string username)
        {
            var userId = this.dbContext.Users.FirstOrDefault(u => u.UserName == username)?.Id;

            if (userId != null)
            {
                var orders = this.dbContext.Orders
                     .Where(o => o.BuyerId == userId)
                     .Select(o => new MyOrdersListServiceModel()
                     {
                         Id = o.Id,
                         Seller = $"{o.Seller.FirstName} {o.Seller.LastName}",
                         TotalPrice = o.Price,
                         IsCompleted = o.IsCompleted,
                         Parts = o.Parts.ToDictionary(p => p.Id, p => p.Name)
                     })
                     .ToList();

                return orders;
            }

            return null;
        }

        public bool IsOrdered(int partId, string username)
            => this.dbContext.Orders
                .Any(o => o.Buyer.UserName == username &&
                o.Parts.Any(p => p.Id == partId));

        private bool IsOwner(int carId, string username)
        {
            var carOwner = this.dbContext.Cars
                .Where(c => c.Id == carId)
                .Select(c => c.Owner.UserName)
                .FirstOrDefault();

            return carOwner == username;
        }
    }
}
