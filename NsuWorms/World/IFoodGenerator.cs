using NsuWorms.MathUtils;
using System.Collections.Generic;

namespace NsuWorms.World
{
    public interface IFoodGenerator
    {
        Vector2Int GenerateFood(IReadOnlyCollection<WorldObject> forbiddenCells);
    }
}
