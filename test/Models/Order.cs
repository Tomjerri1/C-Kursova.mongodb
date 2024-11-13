using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TableId { get; set; } = string.Empty;

        [BsonRequired]
        public DateTime Date { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public string WaiterId { get; set; } = string.Empty;

        [BsonRequired]
        [Range(0.01, 10000, ErrorMessage = "Загальна сума замовлення повинна бути між 0.01 та 10000 гривень")]
        public decimal TotalAmount { get; set; }

        [BsonRequired]
        public bool IsPaid { get; set; } = false;
    }
}
