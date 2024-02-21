using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WDIOU_WEB_API.Models;

namespace WDIOU_WEB_API.Services
{
    public class DebtService
    {
        private readonly IMongoCollection<Debt> _debtsCollection;

        public DebtService(IOptions<WDIOUDatabaseSettings> wdiouDatabaseSettings)
        {
            var mongoClient = new MongoClient(wdiouDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(wdiouDatabaseSettings.Value.DatabaseName);
            _debtsCollection = mongoDatabase.GetCollection<Debt>(wdiouDatabaseSettings.Value.DebtsCollectionName);
        }

        public async Task<List<Debt>> GetDebtsOfUser(string ownr_username) =>
            await _debtsCollection.Find(x => x.owner_nickname == ownr_username).ToListAsync();

        public async Task<Debt?> GetDebtOfUser(string ownr_username, string id) =>
            await _debtsCollection.Find(x => x.owner_nickname == ownr_username && x.Id == id).FirstOrDefaultAsync();

        public async Task CreateDebt(Debt debt) =>
            await _debtsCollection.InsertOneAsync(debt);

        public async Task UpdateDebt(string ownr_username, string id, Debt debt) =>
            await _debtsCollection.ReplaceOneAsync(x => x.owner_nickname == ownr_username && x.Id == id, debt);

        public async Task DeleteDebt(string ownr_username, string id) =>
            await _debtsCollection.DeleteOneAsync(x => x.owner_nickname == ownr_username && x.Id == id);
    }
}
        
           
        

