using NsuWorms.MathUtils;

namespace NsuWorms.World
{
    public class WorldObject
    {
        public Vector2Int Position { get; protected set; }

        public WorldObject(Vector2Int initialPosition)
        {
            Position = initialPosition;
        }
    }
}
