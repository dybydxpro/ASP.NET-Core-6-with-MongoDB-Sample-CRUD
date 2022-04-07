using ClassManagementSystemAPI.Data;
using ClassManagementSystemAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ClassManagementSystemAPI.Services
{
    public class TestServices
    {
        private readonly IMongoCollection<Test> _testCollection;

        public TestServices(
            IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _testCollection = mongoDatabase.GetCollection<Test>("Temp");
        }

        public async Task<List<Test>> GetAsync() =>
            await _testCollection.Find(_ => true).ToListAsync();

        public async Task<Test?> GetAsync(string id) =>
            await _testCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Test test) =>
            await _testCollection.InsertOneAsync(test);

        public async Task UpdateAsync(string id, Test test) =>
            await _testCollection.ReplaceOneAsync(x => x.Id == id, test);

        public async Task RemoveAsync(string id) =>
            await _testCollection.DeleteOneAsync(x => x.Id == id);
    }
}
