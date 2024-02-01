using MongoDB.Bson.Serialization.Attributes;

namespace WDIOU_WEB_API.Models
{
    public class usedEmail
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string email { get; set; }

    }
}
