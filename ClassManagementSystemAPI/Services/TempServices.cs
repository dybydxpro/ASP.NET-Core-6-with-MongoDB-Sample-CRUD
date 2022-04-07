using ClassManagementSystemAPI.Data;
using ClassManagementSystemAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ClassManagementSystemAPI.Services
{
    public class TempServices
    {
        private readonly IMongoCollection<Temp> _tempCollection;

        public TempServices(
            IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _tempCollection = mongoDatabase.GetCollection<Temp>("Temp");
        }

        public async Task<List<Temp>> GetAsync() =>
            await _tempCollection.Find(_ => true).ToListAsync();

        public async Task<Temp?> GetAsync(string id) =>
            await _tempCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Temp temp) =>
            await _tempCollection.InsertOneAsync(temp);

        public async Task UpdateAsync(string id, Temp temp) =>
            await _tempCollection.ReplaceOneAsync(x => x.Id == id, temp);

        public async Task RemoveAsync(string id) =>
            await _tempCollection.DeleteOneAsync(x => x.Id == id);
    }
}
