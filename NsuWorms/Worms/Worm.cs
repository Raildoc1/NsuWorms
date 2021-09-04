using NsuWorms.MathUtils;
using NsuWorms.World;
using NsuWorms.Worms.AI;
using NsuWorms.Worms.AI.Behaviours;
using System;

namespace NsuWorms.Worms
{
    public class Worm : WorldObject
    {
        private IWormBrain _brain;
        private int _health;

        public readonly string Name;

        public int Health
        {
            get => _health;
            private set => _health = Math.Max(0, value);
        }

        public bool Dead => Health == 0;

        public Worm(Vector2Int initialPosition, IWormBrain brain, string name = "New_Worm", int health = 20) : base(initialPosition)
        {
            Health = health;
            Name = name;
            _brain = brain;
        }

        public BehaviourEntity RequestBehaviour(WorldSimulator context)
        {
            return _brain.RequestBehaviour(this, context);
        }

        public void ApplyBehaviour(BehaviourEntity behaviour)
        {
            Health--;

            var changeDirection = behaviour as ChangeDirectionBehaviour;

            if(changeDirection != null)
            {
                Position += changeDirection.PositionDelta;
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
    }
}
