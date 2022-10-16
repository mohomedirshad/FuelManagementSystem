using FuelManagementSystem.Application.Interfaces;
using FuelManagementSystem.BL.Interfaces;

namespace FuelManagementSystem.BL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task UpdateUserArrivalTime(Guid id, DateTime arrivalTime)
        {
            var user = await _userRepository.GetUserById(id);
            if (user is not null)
            {
                user.UpdateArrivalTime(arrivalTime);
                user.SetUpdatedDate();
                await _userRepository.UpdateUser(user);
            }
        }

        public async Task UpdateUserDepartureTime(Guid id, DateTime departureTime)
        {
            var user = await _userRepository.GetUserById(id);
            if (user is not null)
            {
                user.UpdateDepartureTime(departureTime);
                user.SetUpdatedDate();
                await _userRepository.UpdateUser(user);
            }
        }
    }
}
