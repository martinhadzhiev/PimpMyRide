namespace PimpMyRide.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Data.Enums;
    using Data.Models;
    using ImageSharp;
    using Models;

    public class CarService : ICarService
    {
        private readonly PimpMyRideDbContext dbContext;

        public CarService(PimpMyRideDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(User user,
            string make,
            string model,
            int year,
            EngineType engineType,
            BodyType bodyType,
            decimal? totalPrice,
            string description,
            byte[] picture)
        {
            var resizedPicture = picture == null ? null : new Image(picture).Resize(360, 230).ToBase64String();

            var carToAdd = new Car()
            {
                Owner = user,
                Make = make,
                Model = model,
                YearOfProduction = year,
                EngineType = engineType,
                BodyType = bodyType,
                TotalPrice = totalPrice,
                Description = description,
                Picture = resizedPicture,
                Added = DateTime.UtcNow
            };

            this.dbContext.Cars.Add(carToAdd);
            this.dbContext.SaveChanges();

            return carToAdd.Id;
        }

        public User GetOwner(string userName)
            => this.dbContext.Users.FirstOrDefault(u => u.UserName == userName);

        public CarServiceModel GetCar(int id)
            => this.dbContext.Cars.Where(c => c.Id == id)
                .Select(c => new CarServiceModel
                {
                    Make = c.Make,
                    CarModel = c.Model,
                    BodyType = c.BodyType,
                    Description = c.Description,
                    EngineType = c.EngineType,
                    TotalPrice = c.TotalPrice,
                    YearOfProduction = c.YearOfProduction
                })
                .FirstOrDefault();

        public bool IsOwner(int id, string userName)
        {
            if (this.dbContext.Cars.Any(c => c.Id == id))
            {
                var ownerName = this.dbContext.Cars.Where(c => c.Id == id).Select(c => c.Owner.UserName).FirstOrDefault();

                return ownerName == userName;
            }

            return false;
        }

        public int Edit(int id,
            string make,
            string model,
            int yearOfProduction,
            EngineType engineType,
            BodyType bodyType,
            decimal? totalPrice,
            string description,
            byte[] picture)
        {
            var car = this.dbContext.Cars.Find(id);

            var resizedPicture = picture == null ? car.Picture : new Image(picture).Resize(360, 230).ToBase64String();

            if (car != null)
            {
                car.Make = make;
                car.Model = model;
                car.YearOfProduction = yearOfProduction;
                car.EngineType = engineType;
                car.BodyType = bodyType;
                car.TotalPrice = totalPrice;
                car.Description = description;
                car.Picture = resizedPicture;

                this.dbContext.SaveChanges();
            }

            return car?.Id ?? 0;
        }

        public bool Exists(int id)
            => this.dbContext.Cars.Any(c => c.Id == id && !c.IsDeleted);

        public bool IsActive(int id)
           => this.dbContext.Cars.Any(c => c.Id == id && c.IsActive);

        public CarDetailsServiceModel GetCarDetails(int id)
            => this.dbContext.Cars.Where(c => c.Id == id)
                .Select(c => new CarDetailsServiceModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Description = c.Description,
                    TotalPrice = c.TotalPrice,
                    YearOfProduction = c.YearOfProduction,
                    Added = c.Added,
                    BodyType = c.BodyType,
                    EngineType = c.EngineType,
                    Picture = c.Picture,
                    Views = c.Views,
                    OwnerUsername = c.Owner.UserName,
                    Owner = $"{c.Owner.FirstName} {c.Owner.LastName}",
                    PhoneNumber = c.Owner.PhoneNumber,
                    Parts = c.Parts
                })
                .FirstOrDefault();

        public IEnumerable<CarListServiceModel> GetAll()
            => this.dbContext.Cars
            .Where(c => c.IsActive && !c.IsDeleted)
            .Select(c => new CarListServiceModel
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                Added = c.Added,
                Picture = c.Picture
            })
            .ToList();

        public IEnumerable<CarListServiceModel> GetMyCars()
            => this.dbContext.Cars
                .Where(c => !c.IsDeleted)
                .Select(c => new CarListServiceModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Added = c.Added,
                    Picture = c.Picture
                })
                .ToList();

        public void Delete(int id)
        {
            var car = this.dbContext.Cars.Find(id);

            if (car != null)
            {
                car.IsDeleted = true;

                this.dbContext.SaveChanges();
            }
        }

        public void IncreaseViews(int id)
        {
            var car = this.dbContext.Cars
                .FirstOrDefault(c => c.Id == id && c.IsActive && !c.IsDeleted);

            if (car != null)
            {
                car.Views++;

                this.dbContext.SaveChanges();
            }
        }

        public IEnumerable<CarListServiceModel> GetAllSearch(string make, string model, int? @from, int? to)
        {
            var cars = this.dbContext.Cars.Where(c => c.IsActive && !c.IsDeleted).ToList();

            if (make != null)
            {
                cars = cars.Where(c => c.Make.ToLower().Contains(make.ToLower())).ToList();
            }

            if (model != null)
            {
                cars = cars.Where(c => c.Model.ToLower().Contains(model.ToLower())).ToList();
            }

            if (from != null)
            {
                cars = cars.Where(c => c.YearOfProduction >= from).ToList();
            }

            if (to != null)
            {
                cars = cars.Where(c => c.YearOfProduction <= to).ToList();
            }

            return cars.Select(c => new CarListServiceModel
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                Picture = c.Picture,
                Added = c.Added
            })
                .ToList();
        }
    }
}
