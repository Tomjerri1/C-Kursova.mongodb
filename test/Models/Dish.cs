// Models/Dish.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YourNamespace.Models
{
    public class Dish
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public required string Name { get; set; }

        [BsonElement("category")]
        public required string Category { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }
    }
}
