using NsuWorms.MathUtils;
using NsuWorms.World;
using NsuWorms.Worms.AI;
using NsuWorms.Worms.AI.Behaviours;
using System;

namespace NsuWorms.Worms
{
    public class Worm : WorldObject
    {
        private static int count = 0; 

        public const int ReproduceCost = 10;

        private IWormBrain _brain;
        private int _health;

        public static int GlobalCount => count;
        public readonly string Name;

        public int Health
        {
            get => _health;
            private set => _health = Math.Max(0, value);
        }

        public bool Dead => Health == 0;

        public Worm(Vector2Int initialPosition, IWormBrain brain, string name = "New_Worm", int health = 10) : base(initialPosition)
        {
            count++;
            Health = health;
            Name = name;
            _brain = brain;
        }

        public BehaviourEntity RequestBehaviour(WorldSimulatorService context)
        {
            return _brain.RequestBehaviour(this, context);
        }

        public void Tick()
        {
            Health--;
        }

        public void TryApplyChangePositionBehaviour(MoveInDirectionBehaviour changePosition)
        {
            if (changePosition == null)
            {
                return;
            }

            switch (changePosition.Direction)
            {
                case Direction.Up:
                    Position += new Vector2Int(0, 1);
                    break;
                case Direction.Right:
                    Position += new Vector2Int(1, 0);
                    break;
                case Direction.Down:
                    Position += new Vector2Int(0, -1);
                    break;
                case Direction.Left:
                    Position += new Vector2Int(-1, 0);
                    break;
                default:
                    break;
            }
        }

        public void AddHealth(int delta)
        {
            if(delta < 0)
            {
                throw new ArgumentOutOfRangeException("Health delta can't be negative!");
            }

            Health += delta;
        }

        public void Reproduce()
        {
            Health -= ReproduceCost;
        }
    }
}
