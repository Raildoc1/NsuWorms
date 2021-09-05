using Microsoft.Extensions.Hosting;
using NsuWorms.MathUtils;
using NsuWorms.World.FoodGeneration;
using NsuWorms.Worms;
using NsuWorms.Worms.AI;
using NsuWorms.Worms.AI.Behaviours;
using NsuWorms.Writers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NsuWorms.World
{
    public sealed class WorldSimulatorService : IHostedService
    {
        private readonly IWriter _writer;
        private readonly IFoodGenerator _foodGenerator;
        private readonly IWormBrain _wormBrain;
        private readonly IWorld2StringConverter _toStringConverter;

        private List<Worm> _worms = new List<Worm>();
        private List<Food> _foods = new List<Food>();
        private int _foodHealthRecover = 10;

        public IReadOnlyCollection<Worm> Worms => _worms;
        public IReadOnlyCollection<Food> Foods => _foods;

        public WorldSimulatorService(IWriter writer, IFoodGenerator foodGenerator, IWormBrain wormBrain, IWorld2StringConverter converter)
        {
            _writer = writer;
            _foodGenerator = foodGenerator;
            _wormBrain = wormBrain;
            _toStringConverter = converter;

            AddWorm(Vector2Int.Zero, "Ivan");

            WriteData();
        }

        private void AddWorm(Vector2Int position, string name)
        {
            _worms.Add(new Worm(position, _wormBrain, name));
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
            Vector2Int position = _foodGenerator.GenerateFood(Foods);

            var worm = GetWormAt(position);

            if (worm != null)
            {
                worm.AddHealth(_foodHealthRecover);
                return;
            }

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
                case BehaviourType.Null:
                    break;
                case BehaviourType.ChangeDirection:
                    ApplyChangeDirection(worm, behaviour as MoveInDirectionBehaviour);
                    break;
                case BehaviourType.Reproduce:
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
            _writer.WriteLine(_toStringConverter.Convert(this));
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

        public Task StartAsync(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 100; i++)
            {
                Tick();
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
