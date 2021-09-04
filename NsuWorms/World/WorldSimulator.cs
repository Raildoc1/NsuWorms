using NsuWorms.MathUtils;
using NsuWorms.Worms;
using NsuWorms.Worms.AI.Behaviours;
using NsuWorms.Worms.AI.Brains;
using NsuWorms.Writers;
using System;
using System.Collections.Generic;

namespace NsuWorms.World
{
    public class WorldSimulator
    {
        private List<Worm> _worms = new List<Worm>();
        private List<Food> _foods = new List<Food>();
        private IWriter _writer;
        private int _foodHealthRecover = 10;

        public IReadOnlyCollection<Worm> Worms => _worms;
        public IReadOnlyCollection<Food> Foods => _foods;

        public WorldSimulator(IWriter writer)
        {
            AddWorm(Vector2Int.Zero, "Ivan");
            _writer = writer;
            WriteData();
        }

        private void AddWorm(Vector2Int position, string name)
        {
            _worms.Add(new Worm(position, new ChaseClosestFood(), name));
        }

        public void Tick()
        {
            UpdateFood();
            UpdateWorms();
            TryEatFood();
            CheckForDeadWorms();
            WriteData();
        }

        private void UpdateFood()
        {
            foreach (var food in _foods)
            {
                food.Tick();
            }

            _foods.RemoveAll(i => i.LifeTime <= 0);

            GenerateFood();
        }

        private void GenerateFood()
        {
            Random random = new Random();
            Vector2Int position;
            do
            {
                position = new Vector2Int(random.NextNormal(0, 5), random.NextNormal(0, 5));

                var worm = GetWormAt(position);

                if (worm != null)
                {
                    worm.AddHealth(_foodHealthRecover);
                    return;
                }

            } while (!IsCellFree(position));
            _foods.Add(new Food(position));
        }

        private void UpdateWorms()
        {
            var count = _worms.Count;

            for (int i = 0; i < count; i++)
            {
                SimulateWorm(_worms[i]);
            }
        }

        private void SimulateWorm(Worm worm)
        {
            worm.Tick();
            var behaviour = worm.RequestBehaviour(this);

            switch (behaviour.GetBehaviourType())
            {
                case NsuWorms.Worms.AI.BehaviourType.Null:
                    break;
                case NsuWorms.Worms.AI.BehaviourType.ChangeDirection:
                    ApplyChangeDirection(worm, behaviour as MoveInDirectionBehaviour);
                    break;
                case NsuWorms.Worms.AI.BehaviourType.Reproduce:
                    ApplyReproduce(worm, behaviour as ReproduceBehaviour);
                    break;
                default:
                    break;
            }
        }

        private void ApplyChangeDirection(Worm target, MoveInDirectionBehaviour behaviour)
        {
            if (behaviour == null)
            {
                return;
            }

            var desiredPosition = target.Position + DirectionUtils.Direction2Vector(behaviour.Direction);
            if (GetWormAt(desiredPosition) == null)
            {
                target.TryApplyChangePositionBehaviour(behaviour);
            }
        }

        private void ApplyReproduce(Worm target, ReproduceBehaviour behaviour)
        {
            if (behaviour == null)
            {
                return;
            }

            var desiredPosition = target.Position + DirectionUtils.Direction2Vector(behaviour.Direction);

            if (IsCellFree(desiredPosition) && target.Health > Worm.ReproduceCost)
            {
                AddWorm(desiredPosition, $"Ivan{Worm.GlobalCount}");
            }

            target.Reproduce();
        }

        private void TryEatFood()
        {
            foreach (var worm in _worms)
            {
                var food = GetFoodAt(worm.Position);

                if (food != null)
                {
                    _foods.Remove(food);
                    worm.AddHealth(_foodHealthRecover);
                }
            }
        }

        private void CheckForDeadWorms()
        {
            _worms.RemoveAll(i => i.Health == 0);
        }

        private void WriteData()
        {
            var line = "Worms:[";

            var first = true;

            foreach (var worm in _worms)
            {
                if (!first)
                {
                    line += ",";
                }
                line += $"{worm.Name}-{worm.Health}({worm.Position.X},{worm.Position.Y})";
                first = false;
            }

            line += "]";
            line += ",";
            line += "Food:[";

            first = true;

            foreach (var food in _foods)
            {
                if (!first)
                {
                    line += ",";
                }
                line += $"({food.Position.X},{food.Position.Y})";
                first = false;
            }

            line += "]";

            _writer.WriteLine(line);
        }

        public bool IsCellFree(Vector2Int cell)
        {
            return GetObjectAt(cell) == null;
        }

        public WorldObject GetObjectAt(Vector2Int position)
        {
            var worm = GetWormAt(position);

            if (worm != null)
            {
                return worm;
            }

            var food = GetFoodAt(position);

            if (food != null)
            {
                return food;
            }

            return null;
        }

        public Worm GetWormAt(Vector2Int position)
        {
            foreach (var worm in _worms)
            {
                if (worm.Position == position)
                {
                    return worm;
                }
            }

            return null;
        }

        public Food GetFoodAt(Vector2Int position)
        {
            foreach (var food in _foods)
            {
                if (food.Position == position)
                {
                    return food;
                }
            }

            return null;
        }
    }
}
