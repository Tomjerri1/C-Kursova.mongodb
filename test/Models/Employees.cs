using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }


        [Required(ErrorMessage = "Ім'я обов'язкове.")]
        [RegularExpression(@"^[A-Za-zА-Яа-яЁёІіЇї]{1,}$", ErrorMessage = "Введіть лише букви.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Прізвище обов'язкове.")]
        [RegularExpression(@"^[A-Za-zА-Яа-яЁёІіЇї]{1,}$", ErrorMessage = "Введіть лише букви.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Роль обов'язкова.")]
        public string Role { get; set; } = string.Empty;

        public List<string> WorkingDays { get; set; } = new List<string>();

        [Required(ErrorMessage = "Логін обов'язковий.")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль обов'язковий.")]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Пароль має містити від 4 до 16 символів")]
        public string Password { get; set; } = string.Empty;
    }
}
