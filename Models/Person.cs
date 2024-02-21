using MongoDB.Bson.Serialization.Attributes;

namespace WDIOU_WEB_API.Models
{
    public class Person
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string name { get; set; }
        public string owner_nickname { get; set; }
    }
}
