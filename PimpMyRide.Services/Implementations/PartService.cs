namespace PimpMyRide.Services.Implementations
{
    using System.Linq;
    using Contracts;
    using Data;
    using Data.Models;
    using ImageSharp;
    using Models;

    public class PartService : IPartService
    {
        private readonly PimpMyRideDbContext dbContext;

        public PartService(PimpMyRideDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool IsOwner(int carId, string username)
        {
            if (username != null)
            {
                return this.dbContext.Cars
                           .Where(c => c.Id == carId)?
                           .Select(c => c.Owner.UserName)
                           .FirstOrDefault() == username;
            }

            return false;
        }

        public bool CarExists(int carId)
            => this.dbContext.Cars.Any(c => c.Id == carId);

        public bool PartExists(int id)
            => this.dbContext.Parts.Any(p => p.Id == id);

        public void AddPart(int id, string name, decimal? price, string description, byte[] picture)
        {
            var resizedPicture = picture == null ? null : new Image(picture).Resize(150, 100).ToBase64String();

            this.dbContext.Parts.Add(new Part
            {
                CarId = id,
                Name = name,
                Price = price,
                Description = description,
                IsAvailable = true,
                Picture = resizedPicture
            });

            this.dbContext.SaveChanges();
        }

        public void EditPart(int partId, string name, decimal? price, string description, byte[] picture)
        {
           var part = this.dbContext.Parts.Find(partId);

            var resizedPicture = picture == null ? part.Picture : new Image(picture).Resize(150, 100).ToBase64String();

            part.Name = name;
            part.Price = price;
            part.Description = description;
            part.Picture = resizedPicture;

            this.dbContext.SaveChanges();
        }

        public void DeletePart(int id)
        {
            var part = this.dbContext.Parts.Find(id);

            part.IsAvailable = false;

            this.dbContext.SaveChanges();
        }

        public PartCrudServiceModel GetPart(int id)
            => this.dbContext.Parts
            .Where(p => p.Id == id)
            .Select(p => new PartCrudServiceModel
            {
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                CarId = p.CarId
            })
            .FirstOrDefault();

        public PartServiceModel GetParts(int id)
            => this.dbContext.Cars.Where(c => c.Id == id)
                .Select(c => new PartServiceModel
                {
                    OwnerUsername = c.Owner.UserName,
                    Parts = c.Parts
                    .Where(p => p.IsAvailable)
                    .Select(p => new PartListServiceModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Description = p.Description,
                        Picture = p.Picture,
                        CarId = p.CarId
                    })
                    .OrderByDescending(p => p.Id)
                    .ToList()
                })
                .FirstOrDefault();
    }
}
