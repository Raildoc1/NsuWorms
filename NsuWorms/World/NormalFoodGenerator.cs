using NsuWorms.MathUtils;
using System;
using System.Collections.Generic;

namespace NsuWorms.World
{
    public class NormalFoodGenerator : IFoodGenerator
    {
        private Random _random;

        public NormalFoodGenerator(int? seed = null)
        {
            _random = seed == null ? new Random() : new Random((int)seed);
        }

        public Vector2Int GenerateFood(IReadOnlyCollection<WorldObject> forbiddenCells)
        {
            Vector2Int position;
            do
            {
                position = new Vector2Int(_random.NextNormal(0, 5), _random.NextNormal(0, 5));

            } while (!IsCellFree(position, forbiddenCells));
            return position;
        }

        public bool IsCellFree(Vector2Int position, IReadOnlyCollection<WorldObject> forbiddenCells)
        {
            foreach (var cell in forbiddenCells)
            {
                if(position == cell.Position)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
