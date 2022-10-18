using FuelManagementSystem.BL.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelManagementSystem.Application.Interfaces
{
    public interface IFuelStationRepository
    {
        Task<FuelStation> GetFuelStationById(ObjectId id);
        void UpdateFuelStation(FuelStation fuelStation);
        Task<List<FuelStation>> SearchFuelStation(string location);
        Task CreateFuelStation(FuelStation fuelStation);
        Task<int> FuelStationQueueUsersCount(ObjectId fuelStationId);
    }
}
