﻿using System;

namespace NsuWormsWorldBehaviourGenerator.MathUtils
{
    public struct Vector2Int : IEquatable<Vector2Int>
    {
        public int X;
        public int Y;

        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override bool Equals(object other)
        {
            if (!(other is Vector2Int))
            {
                return false;
            }

            return Equals((Vector2Int)other);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ (Y.GetHashCode() << 2);
        }

        public bool Equals(Vector2Int other)
        {
            return X == other.X && Y == other.Y;
        }

        public static float SqrDistance(Vector2Int a, Vector2Int b)
        {
            var deltaX = a.X - b.X;
            var deltaY = a.Y - b.Y;
            return deltaX * deltaX + deltaY * deltaY;
        }

        public static Vector2Int operator +(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2Int operator -(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.X - b.X, a.Y - b.Y);
        }

        public static bool operator ==(Vector2Int lhs, Vector2Int rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y;
        }

        public static bool operator !=(Vector2Int lhs, Vector2Int rhs)
        {
            return !(lhs == rhs);
        }

        public static Vector2Int Zero => _zero;

        private static readonly Vector2Int _zero = new Vector2Int(0, 0);
    }
}
