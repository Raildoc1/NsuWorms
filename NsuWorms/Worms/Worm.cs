using NsuWorms.World;
using NsuWorms.Worms.AI;
using NsuWorms.Worms.AI.Behaviours;
using System.Numerics;

namespace NsuWorms.Worms
{
    public class Worm : WorldObject
    {
        public readonly string Name;
        private IWormBrain _brain;

        public Worm(Vector2 initialPosition, IWormBrain brain, string name = "New_Worm") : base(initialPosition)
        {
            Name = name;
            _brain = brain;
        }

        public BehaviourEntity RequestBehaviour(WorldSimulator context)
        {
            return _brain.RequestBehaviour(this, context);
        }

        public void ApplyBehaviour(BehaviourEntity behaviour)
        {
            var changeDirection = behaviour as ChangeDirectionBehaviour;

            if(changeDirection != null)
            {
                Position += changeDirection.PositionDelta;
            }
        }
    }
}
