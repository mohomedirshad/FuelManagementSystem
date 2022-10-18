using FuelManagementSystem.BL.Entities;
using FuelManagementSystem.BL.Enums;
using MongoDB.Bson;

namespace FuelManagementSystem.Application.Interfaces
{
    public interface IFuelStationService
    {
        Task UpdateAvailabilityFuelStatus(ObjectId fuelStationId, FuelStatus fuelStatus);
        Task UpdateFuelArrivalTime(ObjectId fuelStationId, DateTime fuelArrivalTime);
        Task UpdateFuelFinishTime(ObjectId fuelStationId, DateTime fuelFinishTime);
        Task<List<FuelStation>> SearchFuelStation(string location);
        Task AddUserToFuelStationQueue(ObjectId objectUserId, ObjectId fuelStationId);
        Task RemoveUserFromFuelStationQueue(ObjectId objectUserId, ObjectId fuelStationId);
        Task<int> FuelStationQueueUsersCount(ObjectId fuelStationId);
    }
}
