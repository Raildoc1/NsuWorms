namespace NsuWorms.Models
{
    public class WorldModel
    {
        public WormModel[] Worms { get; set; }
        public FoodModel[] Food { get; set; }
    }

    public class WormModel
    {
        public string Name { get; set; }
        public int LifeStrength { get; set; }
        public PositionModel Position { get; set; }
    }

    public class FoodModel
    {
        public int ExpiresIn { get; set; }
        public PositionModel Position { get; set; }
    }

    public class PositionModel
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
