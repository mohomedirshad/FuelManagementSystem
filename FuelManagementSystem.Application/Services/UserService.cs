using FuelManagementSystem.Application.Interfaces;
using FuelManagementSystem.BL.Entities;
using FuelManagementSystem.BL.Interfaces;
using MongoDB.Bson;

namespace FuelManagementSystem.BL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFuelStationRepository _fuelStationRepository;

        public UserService(IUserRepository userRepository, IFuelStationRepository fuelStationRepository)
        {
            _userRepository = userRepository;
            _fuelStationRepository = fuelStationRepository;
        }
        public async Task UpdateUserArrivalTime(ObjectId userId, ObjectId fuelStationId, DateTime arrivalTime)
        {
            var user = await _userRepository.GetUserById(userId);
            var fuelStation = await _fuelStationRepository.GetFuelStationById(fuelStationId);

            if (user is not null && fuelStation is not null)
            {
                arrivalTime.ToString("MM/dd/yyyy h:mm tt");
                user.UpdateArrivalTime(arrivalTime);
                
                user.SetUpdatedDate();
                _userRepository.UpdateUser(user);
            }
        }

        public async Task UpdateUserDepartureTime(ObjectId id, ObjectId fuelStationId, DateTime departureTime)
        {
            var user = await _userRepository.GetUserById(id);
            var fuelStation = await _fuelStationRepository.GetFuelStationById(fuelStationId);
            if (user is not null && fuelStation is not null)
            {
                departureTime.ToString("MM/dd/yyyy h:mm tt");
                user.UpdateDepartureTime(departureTime);

                user.SetUpdatedDate();
                _userRepository.UpdateUser(user);
            }
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _userRepository.GetUsers();            
            return users;
        }

        public async Task<TimeSpan> GetUserQueueWaitingDuration(ObjectId userObjectId, ObjectId fuelStationId)
        {
            return await _userRepository.GetUsersWaitingTimeDuration(userObjectId, fuelStationId);
        }
    }
}
