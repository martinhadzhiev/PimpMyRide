namespace PimpMyRide.Services.Contracts
{
    using System.Collections.Generic;
    using Data.Models;
    using Models;

    public interface IOrderService
    {
        bool Exists(int partId, string username);

        Part Get(int id);

        User GetUser(int carId);

        User GetUser(string username);

        void AddOrder(string sellerId, string buyerId, IEnumerable<int> parts);

        IEnumerable<MyOrdersListServiceModel> GetOrders(string username);

        bool IsOrdered(int partId, string username);

        void CancelOrder(int orderId, string username);
    }
}