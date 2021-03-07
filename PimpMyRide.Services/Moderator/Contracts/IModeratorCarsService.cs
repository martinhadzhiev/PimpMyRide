namespace PimpMyRide.Services.Moderator.Contracts
{
    using System.Collections.Generic;
    using Models;
    using Services.Models;

    public interface IModeratorCarsService
    {
        IEnumerable<ModeratorCarListModel> GetAllInActive();

        void Approve(int id);

        void Delete(int id);

        CarDetailsServiceModel GetDetails(int id);
    }
}