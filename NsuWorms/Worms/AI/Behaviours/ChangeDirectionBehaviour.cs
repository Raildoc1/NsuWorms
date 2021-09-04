using NsuWorms.MathUtils;

namespace NsuWorms.Worms.AI.Behaviours
{
    public class ChangeDirectionBehaviour : BehaviourEntity
    {
        private Vector2Int _positionDelta = Vector2Int.Zero;
        public Vector2Int PositionDelta => _positionDelta;

        public ChangeDirectionBehaviour(Vector2Int positionDelta)
        {
            _positionDelta = positionDelta;
        }
    }
}
