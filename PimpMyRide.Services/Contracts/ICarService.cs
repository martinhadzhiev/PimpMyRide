namespace PimpMyRide.Services.Contracts
{
    using System.Collections.Generic;
    using Data.Enums;
    using Data.Models;
    using Models;

    public interface ICarService
    {
        int Add(User user,
            string make,
            string model,
            int year,
            EngineType engineType,
            BodyType bodyType,
            decimal? totalPrice,
            string description,
            byte[] picture);

        User GetOwner(string userName);

        CarServiceModel GetCar(int id);

        bool IsOwner(int id, string userName);

        int Edit(int id,
            string make,
            string model,
            int yearOfProduction,
            EngineType engineType,
            BodyType bodyType,
            decimal? totalPrice,
            string description,
            byte[] picture);

        bool Exists(int id);

        bool IsActive(int id);

        CarDetailsServiceModel GetCarDetails(int id);

        IEnumerable<CarListServiceModel> GetAll();

        IEnumerable<CarListServiceModel> GetMyCars();

        void Delete(int id);

        void IncreaseViews(int id);

        IEnumerable<CarListServiceModel> GetAllSearch(string make, string model, int? from, int? to);
    }
}