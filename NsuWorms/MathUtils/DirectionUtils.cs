using System;

namespace NsuWorms.MathUtils
{
    public static class DirectionUtils
    {
        public static Direction Vector2Direction(Vector2Int direction)
        {
            if(direction == new Vector2Int(0 , 1))
            {
                return Direction.Up;
            }

            if(direction == new Vector2Int(0 , -1))
            {
                return Direction.Down;
            }

            if(direction == new Vector2Int(1 , 0))
            {
                return Direction.Right;
            }

            if(direction == new Vector2Int(-1 , 0))
            {
                return Direction.Left;
            }

            throw new ArgumentException("Direction must be unit vector!");
        }

        public static Vector2Int Direction2Vector(Direction direction)
        {
            if(direction == Direction.Up)
            {
                return new Vector2Int(0, 1);
            }

            if(direction == Direction.Down)
            {
                return new Vector2Int(0, -1);
            }

            if(direction == Direction.Right)
            {
                return new Vector2Int(1, 0);
            }

            if(direction == Direction.Left)
            {
                return new Vector2Int(-1, 0);
            }

            throw new ArgumentException("Direction must be unit vector!");
        }
    }
}
