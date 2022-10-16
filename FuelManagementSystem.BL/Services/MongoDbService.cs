using FuelManagementSystem.BL.Entities;
using FuelManagementSystem.DbModelSettings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelManagementSystem.BL.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<User> _userCollection;
        public MongoDbService(IOptions<MongoDBSettings> mongoDbSettings)
        {
            MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionUri);

            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);

            _userCollection = database.GetCollection<User>(mongoDbSettings.Value.CollectionName);
        }

        /// <summary>
        /// need to implement
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAsync() { return null; }

        #region User DB Calls
        public async Task<User> GetUserById(Guid id) 
        {
            //var user = await _userCollection.Find(Builders<User>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
            var user = await _userCollection.Find(u=>u.Id == id).FirstOrDefaultAsync();
            return user;
        }
        public async Task UpdateUserAsync(Guid id,User user)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);            
            await _userCollection.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });
        }

        #endregion
        public async Task CreateAsync(User playlist) { }
        public async Task AddToPlaylistAsync(string id, string movieId) { }
        public async Task DeleteAsync(string id) { }
    }
}
