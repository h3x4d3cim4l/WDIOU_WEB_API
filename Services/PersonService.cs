using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WDIOU_WEB_API.Models;

namespace WDIOU_WEB_API.Services
{
    public class PersonService
    {
        private readonly IMongoCollection<Person> _personCollection;

        public PersonService(IOptions<WDIOUDatabaseSettings> wdiouDatabaseSettings)
        {
            var mongoClient = new MongoClient(wdiouDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(wdiouDatabaseSettings.Value.DatabaseName);
            _personCollection = mongoDatabase.GetCollection<Person>(wdiouDatabaseSettings.Value.PersonCollectionName);
        }

        public async Task<List<Person>> GetPersonsOfUser(string ownr_username)=>
            await _personCollection.Find(x=>x.owner_nickname == ownr_username).ToListAsync();

        public async Task<Person?> GetPersonOfUser(string ownr_username, string pname) =>
            await _personCollection.Find(x => x.owner_nickname == ownr_username && x.name == pname).FirstOrDefaultAsync();

        public async Task CreatePerson(Person person)=>
            await _personCollection.InsertOneAsync(person);

        public async Task UpdatePerson(string ownr_username, string pname , Person person)=>
            await _personCollection.ReplaceOneAsync(x=>x.owner_nickname == ownr_username && x.name == pname, person);

        public async Task DeletePerson(string ownr_username, string pname) =>
            await _personCollection.DeleteOneAsync(x => x.owner_nickname == ownr_username && x.name == pname);
        
           
        

           

    }
}
