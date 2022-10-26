using FuelManagementSystem.Application.Interfaces;
using FuelManagementSystem.BL.Entities;
using FuelManagementSystem.Data.Dto;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace FuelManagementSystem.Data.Repository
{
    public class FuelStationRepository: IFuelStationRepository
    {
        private readonly IConfiguration _configuration;

        public FuelStationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private MongoClient GetMongoDbClient()
        {
            MongoClient client = new(_configuration.GetConnectionString("MongoDbConnection"));
            return client;
        }

        public async Task<FuelStation> GetFuelStationById(ObjectId id)
        {
            var client = GetMongoDbClient();
            var fuelStation = await client.GetDatabase("EAD").GetCollection<FuelStation>("fuelStations")
                .Find(Builders<FuelStation>.Filter.Eq("Id", id))
                .FirstOrDefaultAsync();
            return fuelStation;

        }

        public void UpdateFuelStation(FuelStation fuelStation)
        {
            var client = GetMongoDbClient();
            FilterDefinition<FuelStation> filter = Builders<FuelStation>.Filter.Eq("_id", fuelStation.Id);
            client.GetDatabase("EAD").GetCollection<FuelStation>("fuelStations").ReplaceOne(filter, fuelStation);
        }

        public async Task CreateFuelStation(FuelStation fuelStation)
        {
            var client = GetMongoDbClient();
            await client.GetDatabase("EAD").GetCollection<FuelStation>("fuelStations").InsertOneAsync(fuelStation);
        }

        
        public async Task<List<FuelStation>> SearchFuelStation(string location)
        {
            var client = GetMongoDbClient();
            
            return await client.GetDatabase("EAD").GetCollection<FuelStation>("fuelStations")
                .AsQueryable().Where(l=>l.Location.ToLower().Contains(location))
                .ToListAsync();
        }

        public async Task<UserCountDto> FuelStationQueueUsersCount(ObjectId fuelStationId)
        {
            var client = GetMongoDbClient();
            var fuelStation = await client.GetDatabase("EAD").GetCollection<FuelStation>("fuelStations")
                .Find(Builders<FuelStation>.Filter.Eq("Id", fuelStationId))
                .FirstOrDefaultAsync();

            return new UserCountDto { UsersCount = fuelStation.UserIds.Count };
        }

        public async Task<List<FuelStation>> GetFuelStations()
        {
            var client = GetMongoDbClient();
            var fuelStations = await client.GetDatabase("EAD").GetCollection<FuelStation>("fuelStations").AsQueryable().ToListAsync();
            return fuelStations;

        }
    }
}
