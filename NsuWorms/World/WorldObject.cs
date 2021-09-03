using System.Numerics;

namespace NsuWorms.World
{
    public class WorldObject
    {
        public Vector2 Position { get; protected set; }

        public WorldObject(Vector2 initialPosition)
        {
            Position = initialPosition;
        }
    }
}
