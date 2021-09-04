using NsuWorms.MathUtils;
using NsuWorms.World;
using NsuWorms.Worms.AI.Behaviours;

namespace NsuWorms.Worms.AI.Brains
{
    public class ClockWiseMovement : IWormBrain
    {
        private bool _isFirstMove = true;
        private int _step = -1;

        private Direction[] _directions =  {
            Direction.Right,
            Direction.Down,
            Direction.Down,
            Direction.Left,
            Direction.Left,
            Direction.Up,
            Direction.Up,
            Direction.Right
        };

        public BehaviourEntity RequestBehaviour(Worm target, WorldSimulator world)
        {
            if (_isFirstMove)
            {
                _isFirstMove = false;
                return new MoveInDirectionBehaviour(Direction.Up);
            }

            _step = (_step + 1) % _directions.Length;

            return new MoveInDirectionBehaviour(_directions[_step]);
        }
    }
}
