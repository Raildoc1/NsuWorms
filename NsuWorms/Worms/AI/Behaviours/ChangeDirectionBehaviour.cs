using System.Numerics;

namespace NsuWorms.Worms.AI.Behaviours
{
    public class ChangeDirectionBehaviour : BehaviourEntity
    {
        private Vector2 _positionDelta = Vector2.Zero;
        public Vector2 PositionDelta => _positionDelta;

        public ChangeDirectionBehaviour(Vector2 positionDelta)
        {
            _positionDelta = positionDelta;
        }
    }
}
