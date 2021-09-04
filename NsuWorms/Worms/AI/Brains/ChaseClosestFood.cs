using NsuWorms.MathUtils;
using NsuWorms.World;
using NsuWorms.Worms.AI.Behaviours;
using System;

namespace NsuWorms.Worms.AI.Brains
{
    public class ChaseClosestFood : IWormBrain
    {
        public BehaviourEntity RequestBehaviour(Worm target, WorldSimulatorService world)
        {
            if(world.Foods.Count == 0)
            {
                return new NullBehaviour();
            }

            var closest = FindClosestFood(target.Position, world);

            var direction = closest - target.Position;

            if(Math.Abs(direction.X) > Math.Abs(direction.Y))
            {
                return new MoveInDirectionBehaviour(DirectionUtils.Vector2Direction(new Vector2Int(Math.Sign(direction.X), 0)));
            }

            return new MoveInDirectionBehaviour(DirectionUtils.Vector2Direction(new Vector2Int(0, Math.Sign(direction.Y))));
        }

        private Vector2Int FindClosestFood(Vector2Int wormPosition, WorldSimulatorService world)
        {
            var minSqrDistance = float.PositiveInfinity;
            var closest = wormPosition;

            foreach (var food in world.Foods)
            {
                var sqrDistance = Vector2Int.SqrDistance(wormPosition, food.Position);
                if(sqrDistance < minSqrDistance)
                {
                    minSqrDistance = sqrDistance;
                    closest = food.Position;
                }
            }

            return closest;
        }
    }
}
