namespace NsuWorms.Database
{
    public interface IDatabaseFoodReader
    {
        string GetFoodsString(BehavioursDbContext database);
    }
}
