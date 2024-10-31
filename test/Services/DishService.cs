// Services/DishService.cs
using MongoDB.Driver;
using YourNamespace.Models;

namespace YourNamespace.Services
{
    public class DishService
    {
        private readonly IMongoCollection<Dish> _dishesCollection;

        public DishService(IConfiguration configuration)
        {
            var mongoClient = new MongoClient(configuration["MongoDBSettings:ConnectionString"]);
            var mongoDatabase = mongoClient.GetDatabase(configuration["MongoDBSettings:DatabaseName"]);
            _dishesCollection = mongoDatabase.GetCollection<Dish>("Dishes");
        }

        public async Task AddDishAsync(Dish dish)
        {
            await _dishesCollection.InsertOneAsync(dish);
        }
    }
}
