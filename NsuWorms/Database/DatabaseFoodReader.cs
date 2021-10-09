using System;

namespace NsuWorms.Database
{
    public sealed class DatabaseFoodReader : IDatabaseFoodReader
    {
        private string _behaviourName;

        public DatabaseFoodReader(string behaviourName)
        {
            _behaviourName = behaviourName;
        }

        public string GetFoodsString(BehavioursDbContext database)
        {
            Console.WriteLine($"Trying to get food from database...");

            foreach (var behaviour in database.Behaviours)
            {
                if (behaviour.Id.Equals(_behaviourName))
                {
                    Console.WriteLine($"Behaviour successfully found!");
                    return behaviour.Points;
                }
            }

            Console.WriteLine($"Failed to find!");
            return string.Empty;
        }
    }
}
