using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WDIOU_WEB_API.Models;

namespace WDIOU_WEB_API.Services
{
    public class UsersService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UsersService(IOptions<WDIOUDatabaseSettings> wdiouDatabaseSettings)
        {
            var mongoClient = new MongoClient(wdiouDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(wdiouDatabaseSettings.Value.DatabaseName);
            _usersCollection = mongoDatabase.GetCollection<User>(wdiouDatabaseSettings.Value.UsersCollectionName);
        }

        public async Task<List<User>> GetUsersAsync() =>
            await _usersCollection.Find(_=> true).ToListAsync();

        public async Task<User?> GetUserAsync(string id) =>
            await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateUserAsync(User newUser) =>
            await _usersCollection.InsertOneAsync(newUser);

        public async Task UpdateUserAsync(string id, User updatedUser) =>
            await _usersCollection.ReplaceOneAsync(x=>x.Id == id, updatedUser);

        public async Task RemoveUserAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);

    }
}
