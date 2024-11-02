using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YourNamespace.Models
{
    public class Key
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired]
        public string Username { get; set; } = string.Empty;

        [BsonRequired]
        public string Password { get; set; } = string.Empty;
    }
}
