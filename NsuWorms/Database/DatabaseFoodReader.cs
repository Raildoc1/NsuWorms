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
            foreach (var behaviour in database.Behaviours)
            {
                if(behaviour.Id.Equals(_behaviourName))
                {
                    return behaviour.Points;
                }
            }

            return string.Empty;
        }
    }
}
