namespace PimpMyRide.Services.Contracts
{
    using Models;

    public interface IPartService
    {
        bool IsOwner(int carId, string username);

        bool CarExists(int carId);

        bool PartExists(int id);

        void AddPart(int id, string name, decimal? price, string description, byte[] picture);

        void EditPart(int partId, string name, decimal? price, string description, byte[] picture);

        void DeletePart(int id);

        PartCrudServiceModel GetPart(int id);

        PartServiceModel GetParts(int id);
    }
}