using NsuWorms.MathUtils;

namespace NsuWorms.Worms.AI.Behaviours
{
    public class MoveInDirectionBehaviour : BehaviourEntity
    {
        private Direction _positionDelta;
        public Direction Direction => _positionDelta;

        public MoveInDirectionBehaviour(Direction positionDelta)
        {
            _positionDelta = positionDelta;
        }

        public override BehaviourType GetBehaviourType()
        {
            return BehaviourType.ChangeDirection;
        }
    }
}
