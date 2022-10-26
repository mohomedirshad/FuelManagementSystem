using FuelManagementSystem.Application.Interfaces;
using FuelManagementSystem.BL.Entities;
using FuelManagementSystem.BL.Enums;
using FuelManagementSystem.Data.Dto;
using MongoDB.Bson;

namespace FuelManagementSystem.Application.Services
{
    public class FuelStationService: IFuelStationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFuelStationRepository _fuelStationRepository;

        public FuelStationService(IUserRepository userRepository, IFuelStationRepository fuelStationRepository)
        {
            _userRepository = userRepository;
            _fuelStationRepository = fuelStationRepository;
        }

        public async Task UpdateAvailabilityFuelStatus(ObjectId fuelStationId,FuelStatus fuelStatus)
        {
            var fuelStation = await _fuelStationRepository.GetFuelStationById(fuelStationId);

            if (fuelStation is not null)
            {
                fuelStation.UpdateFuelAvailabilityStatus(fuelStatus);
                fuelStation.SetUpdatedDate();

                _fuelStationRepository.UpdateFuelStation(fuelStation);
            }
        }

        public async Task UpdateFuelArrivalTime(ObjectId fuelStationId, string fuelArrivalTime)
        {
            var fuelStation = await _fuelStationRepository.GetFuelStationById(fuelStationId);

            if (fuelStation is not null)
            {
                fuelStation.SetFuelArrivalTime(fuelArrivalTime);
                fuelStation.SetUpdatedDate();

                _fuelStationRepository.UpdateFuelStation(fuelStation);
            }
        }

        public async Task UpdateFuelFinishTime(ObjectId fuelStationId, string fuelFinishTime)
        {
            var fuelStation = await _fuelStationRepository.GetFuelStationById(fuelStationId);

            if (fuelStation is not null)
            {
                fuelStation.SetFuelFinishTime(fuelFinishTime);
                fuelStation.SetUpdatedDate();

                _fuelStationRepository.UpdateFuelStation(fuelStation);
            }
        }
        
        public async Task AddUserToFuelStationQueue(ObjectId objectUserId, ObjectId fuelStationId)
        {
            var fuelStation = await _fuelStationRepository.GetFuelStationById(fuelStationId);
            var user = await _userRepository.GetUserById(objectUserId);

            if (fuelStation is not null && user is not null)
            {
                var userId = objectUserId.ToString();
                fuelStation.AddUserToFuelStationQueue(userId);

                fuelStation.SetUpdatedDate();

                _fuelStationRepository.UpdateFuelStation(fuelStation);
            }
        }

        public async Task RemoveUserFromFuelStationQueue(ObjectId objectUserId, ObjectId fuelStationId)
        {
            var fuelStation = await _fuelStationRepository.GetFuelStationById(fuelStationId);
            var user = await _userRepository.GetUserById(objectUserId);

            if (fuelStation is not null && user is not null)
            {
                var userId = objectUserId.ToString();
                fuelStation.RemoveUserFromFuelStationQueue(userId);

                fuelStation.SetUpdatedDate();

                _fuelStationRepository.UpdateFuelStation(fuelStation);
            }
        }

        public async Task<List<FuelStation>>SearchFuelStation(string location)
        {
            return await _fuelStationRepository.SearchFuelStation(location);
        }

        public async Task<UserCountDto> FuelStationQueueUsersCount(ObjectId fuelStationId)
        {
            var fuelStation = await _fuelStationRepository.GetFuelStationById(fuelStationId);

            if (fuelStation is null)
            {
                throw new Exception("Fuel station is not found");
            }
            return await _fuelStationRepository.FuelStationQueueUsersCount(fuelStation.Id);
        }

        public async Task<FuelStation> GetFuelStationById(ObjectId id)
        {
            return await _fuelStationRepository.GetFuelStationById(id);
        }

        public async Task<List<FuelStation>> GetFuelStations()
        {
            return await _fuelStationRepository.GetFuelStations();
        }
    }
}
