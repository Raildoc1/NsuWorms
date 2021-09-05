namespace NsuWorms.World
{
    public sealed class World2StringConverter : IWorld2StringConverter
    {
        public string Convert(WorldSimulatorService world)
        {
            var line = "Worms:[";

            var first = true;

            foreach (var worm in world.Worms)
            {
                if (!first)
                {
                    line += ",";
                }
                line += $"{worm.Name}-{worm.Health}({worm.Position.X},{worm.Position.Y})";
                first = false;
            }

            line += "]";
            line += ",";
            line += "Food:[";

            first = true;

            foreach (var food in world.Foods)
            {
                if (!first)
                {
                    line += ",";
                }
                line += $"({food.Position.X},{food.Position.Y})";
                first = false;
            }

            line += "]";

            return line;
        }
    }
}
