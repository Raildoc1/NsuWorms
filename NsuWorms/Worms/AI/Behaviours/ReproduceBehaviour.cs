using NsuWorms.MathUtils;

namespace NsuWorms.Worms.AI.Behaviours
{
    public class ReproduceBehaviour : BehaviourEntity
    {
        private Direction _direction;
        public Direction Direction => _direction;

        public ReproduceBehaviour(Direction direction)
        {
            _direction = direction;
        }

        public override BehaviourType GetBehaviourType()
        {
            return BehaviourType.Reproduce;
        }
    }
}
