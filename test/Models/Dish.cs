using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Models
{
    public class Dish
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [Required(ErrorMessage = "Назва страви є обов'язковою")]
        [StringLength(100, ErrorMessage = "Назва страви не повинна перевищувати 100 символів")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Категорія є обов'язковою")]
        public required string Category { get; set; }

        [Required(ErrorMessage = "Ціна є обов'язковою")]
        [Range(0.01, 10000, ErrorMessage = "Ціна повинна бути між 0.01 та 10000 гривень")]
        public decimal Price { get; set; }
    }
}
