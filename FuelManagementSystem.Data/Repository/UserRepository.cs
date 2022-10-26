using FuelManagementSystem.Application.Dto;
using FuelManagementSystem.Application.Interfaces;
using FuelManagementSystem.BL.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Web.Mvc;

namespace FuelManagementSystem.Data.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private MongoClient GetMongoDbClient()
        {
            MongoClient client = new(_configuration.GetConnectionString("MongoDbConnection"));
            return client;
        }
        public async Task AddNewUser(User user)
        {
            var client = GetMongoDbClient();
            await client.GetDatabase("EAD").GetCollection<User>("users").InsertOneAsync(user);
        }

        public async Task<User> ValidateUser(LoginDto loginDto)
        {
            var client = GetMongoDbClient();
            var user = await client.GetDatabase("EAD").GetCollection<User>("users")
                .Find(s=>s.Name == loginDto.Username && s.Password == loginDto.Password)
                .FirstOrDefaultAsync();
            return user;
        }

        public void UpdateUser(User user)
        {
            var client = GetMongoDbClient();
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("_id", user.Id);
            client.GetDatabase("EAD").GetCollection<User>("users").ReplaceOne(filter, user);
        }
        public async Task<User> GetUserById(ObjectId id)
        {
            var client = GetMongoDbClient();
            var user = await client.GetDatabase("EAD").GetCollection<User>("users")
                .Find(Builders<User>.Filter.Eq("_id", id))
                .FirstOrDefaultAsync();
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            var client = GetMongoDbClient();
            var users = client.GetDatabase("EAD").GetCollection<User>("users").AsQueryable();
            return users;
        }

        public async Task<TimeSpan> GetUsersWaitingTimeDuration(ObjectId userObjectId, ObjectId fuelStationId)
        {
            var client = GetMongoDbClient();
            var user = await client.GetDatabase("EAD").GetCollection<User>("users")
                .Find(Builders<User>.Filter.Eq("_id", userObjectId))
                .FirstOrDefaultAsync();

            if (user is null)
            {
                throw new Exception("User is not found");
            }

            var fuelStation = await client.GetDatabase("EAD").GetCollection<FuelStation>("fuelStations")
                .Find(Builders<FuelStation>.Filter.Eq("_id", fuelStationId))
                .FirstOrDefaultAsync();

            if (fuelStation is null)
            {
                throw new Exception("Fuel station is not found");
            }

            var isAvailable = fuelStation.UserIds.Where(s => s.Contains(userObjectId.ToString())).Any();

            if (isAvailable)
            {
                var arrivedTime = DateTime.ParseExact(user.ArrivalTime, "MM/dd/yyyy h:mm tt", null);
                if (!string.IsNullOrWhiteSpace(user.DepartureTime))
                {
                    var departureTime = DateTime.ParseExact(user.DepartureTime, "MM/dd/yyyy h:mm tt", null);
                    return departureTime - arrivedTime;
                }
                var currentTime = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
                return DateTime.ParseExact(currentTime, "MM/dd/yyyy h:mm tt", null) - arrivedTime;

            }
            return TimeSpan.Zero;
        }
    }
}
