using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WDIOU_WEB_API.Models;

namespace WDIOU_WEB_API.Services
{
    public class usedEmailsService
    {

        private readonly IMongoCollection<usedEmail> _usedEmailsCollection;

        public usedEmailsService(IOptions<WDIOUDatabaseSettings> wdiouDatabaseSettings) 
        {
            var mongoClient = new MongoClient(wdiouDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(wdiouDatabaseSettings.Value.DatabaseName);
            _usedEmailsCollection = mongoDatabase.GetCollection<usedEmail>(wdiouDatabaseSettings.Value.usedEmailsCollectionName);
        }

        public async Task<List<usedEmail>> GetUsedEmails() =>
            await _usedEmailsCollection.Find(_ => true).ToListAsync();

        public async Task<usedEmail?> GetUsedEmail(string email) =>
            await _usedEmailsCollection.Find(x => x.email == email).FirstOrDefaultAsync();

        public async Task CreateUsedEmail(usedEmail newUsedEmail)=>
            await _usedEmailsCollection.InsertOneAsync(newUsedEmail);

        public async Task UpdateUsedEmail(string email, usedEmail newUsedEmail) =>
            await _usedEmailsCollection.ReplaceOneAsync(x => x.email == email, newUsedEmail);

        public async Task DeleteUsedEmail(string email) =>
            await _usedEmailsCollection.DeleteOneAsync(x => x.email == email);
    }
}
