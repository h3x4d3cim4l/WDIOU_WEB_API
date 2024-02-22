using MongoDB.Bson.Serialization.Attributes;
namespace WDIOU_WEB_API.Models
{
    public class Debt
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int value { get; set; }
        public string sign { get; set; }
        public string owner_nickname { get; set; }
        public string person_nickname { get; set; }
        public string add_date { get; set; }
        public string due_date { get; set; }

    }
}
