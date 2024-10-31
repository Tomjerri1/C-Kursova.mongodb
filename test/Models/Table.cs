using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Models
{
    public class Table
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired]
        [Range(1, int.MaxValue, ErrorMessage = "Номер столика має бути більшим або рівним 1.")]
        public int TableNumber { get; set; }

        [BsonDefaultValue(true)] // Додаємо значення за замовчуванням
        public bool IsAvailable { get; set; } // Ця властивість не обов'язкова для вказування при створенні
    }
}
