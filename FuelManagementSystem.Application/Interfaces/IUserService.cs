using FuelManagementSystem.Application.Dto;
using FuelManagementSystem.BL.Entities;
using MongoDB.Bson;

namespace FuelManagementSystem.BL.Interfaces
{
    public interface IUserService
    {
        Task UpdateUserArrivalTime(ObjectId userId, ObjectId fuelStationId);
        Task UpdateUserDepartureTime(ObjectId id, ObjectId fuelStationId);
        IEnumerable<User> GetUsers();
        Task<TimeSpan> GetUserQueueWaitingDuration(ObjectId userObjectId, ObjectId fuelStationId);
        Task AddNewUser(UserRegistrationDto userRegistrationDto);
        Task<User> ValidateUser(LoginDto loginDto);
    }
}
