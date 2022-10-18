using FuelManagementSystem.BL.Entities;
using MongoDB.Bson;

namespace FuelManagementSystem.BL.Interfaces
{
    public interface IUserService
    {
        Task UpdateUserArrivalTime(ObjectId userId, ObjectId fuelStationId, DateTime arrivalTime);
        Task UpdateUserDepartureTime(ObjectId id, ObjectId fuelStationId, DateTime departureTime);
        IEnumerable<User> GetUsers();
        Task<TimeSpan> GetUserQueueWaitingDuration(ObjectId userObjectId, ObjectId fuelStationId);
    }
}
