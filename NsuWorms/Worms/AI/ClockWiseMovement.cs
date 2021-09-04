using NsuWorms.Math;
using NsuWorms.World;
using NsuWorms.Worms.AI.Behaviours;

namespace NsuWorms.Worms.AI
{
    public class ClockWiseMovement : IWormBrain
    {
        private bool _isFirstMove = true;
        private int _step = -1;

        private Vector2Int[] _directions =  {
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, 1),
            new Vector2Int(1, 0)
        };

        public BehaviourEntity RequestBehaviour(Worm target, WorldSimulator world)
        {
            if (_isFirstMove)
            {
                _isFirstMove = false;
                return new ChangeDirectionBehaviour(new Vector2Int(0, 1));
            }

            _step = (_step + 1) % _directions.Length;

            return new ChangeDirectionBehaviour(_directions[_step]);
        }
    }
}
