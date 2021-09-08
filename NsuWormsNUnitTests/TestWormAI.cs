using NsuWorms.MathUtils;
using NsuWorms.World;
using NsuWorms.Worms;
using NsuWorms.Worms.AI;
using NsuWorms.Worms.AI.Behaviours;
using System;

namespace NsuWormsNUnitTests
{
    public class TestWormAI : IWormBrain
    {
        private bool _firstMove = true;
        private int _currentMove = 0;
        private bool _mustReproduce = true;
        private Direction _lastReproduceDirection;

        public TestWormAI(int initialMove)
        {
            _currentMove = initialMove;
        }

        public BehaviourEntity RequestBehaviour(Worm target, WorldSimulatorService world)
        {
            if (_firstMove)
            {
                _firstMove = false;
                return new NullBehaviour();
            }

            if (_mustReproduce)
            {
                _mustReproduce = !_mustReproduce;
                var values = Enum.GetValues(typeof(Direction));
                var direciton = (Direction)values.GetValue(_currentMove % values.Length);
                _currentMove++;
                _lastReproduceDirection = direciton;
                return new ReproduceBehaviour(direciton);
            }
            else
            {
                _mustReproduce = !_mustReproduce;
                Console.WriteLine("Trying to move...");
                return new MoveInDirectionBehaviour(_lastReproduceDirection);
            }
        }
    }
}
