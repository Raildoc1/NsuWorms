using NsuWorms.Database;

namespace NsuWorms.World.FoodGeneration
{
    public sealed class DatabaseFoodLoader : IFoodDataLoader
    {
        private IDatabaseFoodReader _reader;
        private BehavioursDbContext _database;

        public DatabaseFoodLoader(IDatabaseFoodReader reader, BehavioursDbContext database)
        {
            _reader = reader;
            _database = database;
        }

        public string Load()
        {
            return _reader.GetFoodsString(_database);
        }
    }
}
