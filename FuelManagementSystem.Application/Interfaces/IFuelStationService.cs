using FuelManagementSystem.BL.Entities;
using FuelManagementSystem.BL.Enums;
using FuelManagementSystem.Data.Dto;
using MongoDB.Bson;

namespace FuelManagementSystem.Application.Interfaces
{
    public interface IFuelStationService
    {
        Task UpdateAvailabilityFuelStatus(ObjectId fuelStationId, FuelStatus fuelStatus);
        Task UpdateFuelArrivalTime(ObjectId fuelStationId, string fuelArrivalTime);
        Task UpdateFuelFinishTime(ObjectId fuelStationId, string fuelFinishTime);
        Task<List<FuelStation>> SearchFuelStation(string location);
        Task AddUserToFuelStationQueue(ObjectId objectUserId, ObjectId fuelStationId);
        Task RemoveUserFromFuelStationQueue(ObjectId objectUserId, ObjectId fuelStationId);
        Task<UserCountDto> FuelStationQueueUsersCount(ObjectId fuelStationId);
        Task<FuelStation> GetFuelStationById(ObjectId id);
        Task<List<FuelStation>> GetFuelStations();
    }
}
