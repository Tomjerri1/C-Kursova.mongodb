using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YourNamespace.Models
{
    public class Employee
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired]
        public string FirstName { get; set; } = string.Empty;

        [BsonRequired]
        public string LastName { get; set; } = string.Empty;

        [BsonRequired]
        public string Role { get; set; } = string.Empty;

        public List<string> WorkDays { get; set; } = new();

        [BsonRequired]
        public ObjectId KeyId { get; set; }
    }
}
