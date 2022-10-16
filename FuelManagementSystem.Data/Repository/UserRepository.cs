using FuelManagementSystem.Application.Interfaces;
using FuelManagementSystem.BL.Entities;
using FuelManagementSystem.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelManagementSystem.Data.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly MongoDbService _mongoDbService;

        public UserRepository(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        public async Task UpdateUser(User user)
        {
            await _mongoDbService.UpdateUserAsync(user.Id, user);
        }
        public async Task<User> GetUserById(Guid id)
        {
            var user = await _mongoDbService.GetUserById(id);
            return user;

        }
    }
}
