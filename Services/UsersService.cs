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

        public async Task<User?> GetUserAsync(string username) =>
            await _usersCollection.Find(x => x.Username == username).FirstOrDefaultAsync();

        public async Task CreateUserAsync(User newUser) =>
            await _usersCollection.InsertOneAsync(newUser);

        public async Task UpdateUserAsync(string username, User updatedUser) =>
            await _usersCollection.ReplaceOneAsync(x=>x.Username == username, updatedUser);

        public async Task RemoveUserAsync(string username) =>
            await _usersCollection.DeleteOneAsync(x => x.Username == username);

    }
}
