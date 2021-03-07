namespace PimpMyRide.Services.Moderator.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Models;
    using Services.Models;

    public class ModeratorCarsService : IModeratorCarsService
    {
        private readonly PimpMyRideDbContext dbContext;

        public ModeratorCarsService(PimpMyRideDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<ModeratorCarListModel> GetAllInActive()
            => this.dbContext.Cars
                .Where(c => !c.IsActive && !c.IsDeleted)
                .Select(c => new ModeratorCarListModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Picture = c.Picture,
                    Added = c.Added,
                    Parts = c.Parts.Select(p => new ModeratorPartListModel
                    {
                        Name = p.Name,
                        Price = p.Price,
                        Description = p.Description,
                        Picture = p.Picture
                    })
                    .ToList()
                })
                .ToList();

        public void Approve(int id)
        {
            var car = this.dbContext.Cars.Find(id);

            if (car != null)
            {
                car.IsActive = true;
                this.dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var car = this.dbContext.Cars.Find(id);

            if (car != null)
            {
                this.dbContext.Cars.Remove(car);

                this.dbContext.SaveChanges();
            }
        }

        public CarDetailsServiceModel GetDetails(int id)
            => this.dbContext.Cars
                .Where(c => c.Id == id)
                .Select(c => new CarDetailsServiceModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    Parts = c.Parts,
                    Owner = $"{c.Owner.FirstName} {c.Owner.LastName}",
                    Picture = c.Picture,
                    Make = c.Make,
                    Description = c.Description,
                    Added = c.Added,
                    BodyType = c.BodyType,
                    YearOfProduction = c.YearOfProduction,
                    Views = c.Views,
                    EngineType = c.EngineType,
                    OwnerUsername = c.Owner.UserName,
                    PhoneNumber = c.Owner.PhoneNumber
                })
                .FirstOrDefault();
    }
}