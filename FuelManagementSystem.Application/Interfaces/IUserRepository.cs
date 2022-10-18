using FuelManagementSystem.BL.Entities;
using MongoDB.Bson;

namespace FuelManagementSystem.Application.Interfaces
{
    public interface IUserRepository
    {
        void UpdateUser(User user);
        Task<User> GetUserById(ObjectId id);
        IEnumerable<User> GetUsers();
        Task<TimeSpan> GetUsersWaitingTimeDuration(ObjectId userObjectId, ObjectId fuelStationId);
    }
}
