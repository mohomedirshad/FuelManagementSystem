using FuelManagementSystem.Application.Dto;
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
        public async Task UpdateUserArrivalTime(ObjectId userId, ObjectId fuelStationId)
        {
            var user = await _userRepository.GetUserById(userId);
            var fuelStation = await _fuelStationRepository.GetFuelStationById(fuelStationId);

            if (user is not null && fuelStation is not null)
            {
                user.UpdateArrivalTime();
                
                user.SetUpdatedDate();
                _userRepository.UpdateUser(user);
            }
        }

        public async Task UpdateUserDepartureTime(ObjectId id, ObjectId fuelStationId)
        {
            var user = await _userRepository.GetUserById(id);
            var fuelStation = await _fuelStationRepository.GetFuelStationById(fuelStationId);
            if (user is not null && fuelStation is not null)
            {
                user.UpdateDepartureTime();

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

        public async Task AddNewUser(UserRegistrationDto userRegistrationDto)
        {
            var user = new User
            {
                Name = userRegistrationDto.Name,
                CreatedOn = DateTime.Now.ToString("MM/dd/yyyy h:mm tt"),
                UserType = userRegistrationDto.UserType,
                VehicleType = userRegistrationDto.VehicleType,
                ArrivalTime = string.Empty,
                DepartureTime = string.Empty,
                UpdatedOn = DateTime.Now.ToString("MM/dd/yyyy h:mm tt"),
                Password = userRegistrationDto.Password,
            };
            
            await _userRepository.AddNewUser(user);
        }

        public async Task<User> ValidateUser(LoginDto loginDto)
        {
            return await _userRepository.ValidateUser(loginDto);
        }
    }
}
