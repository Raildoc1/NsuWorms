using NsuWorms.MathUtils;
using System.Collections.Generic;

namespace NsuWorms.World.FoodGeneration
{
    public interface IFoodGenerator
    {
        Vector2Int GenerateFood(IReadOnlyCollection<WorldObject> forbiddenCells);
    }
}
