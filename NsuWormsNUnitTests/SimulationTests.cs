using NsuWorms.MathUtils;
using NsuWorms.World;
using NsuWorms.Worms;
using NsuWorms.Worms.AI;
using NsuWorms.Worms.AI.Behaviours;
using NsuWorms.Worms.AI.Brains;
using NsuWorms.Writers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NsuWormsNUnitTests
{
    public class Tests
    {
        [Test]
        public void EmptyCellMovement()
        {
            WorldSimulatorService worldSimulator = new WorldSimulatorService(
                new ConsoleWriter(),
                new NormalFoodGenerator(),
                new ClockWiseMovement(),
                new World2StringConverter()
                );

            // First worm starts on (0, 0)
            Assert.AreEqual(worldSimulator.Worms.Count, 1);
            Assert.AreEqual(worldSimulator.Worms.ElementAt(0).Position, new Vector2Int(0, 0));

            // And moves clockwise
            worldSimulator.Tick();
            Assert.AreEqual(worldSimulator.Worms.ElementAt(0).Position, new Vector2Int(0, 1));

            worldSimulator.Tick();
            Assert.AreEqual(worldSimulator.Worms.ElementAt(0).Position, new Vector2Int(1, 1));
        }

        [Test]
        public void EatFood()
        {
            WorldSimulatorService worldSimulator = new WorldSimulatorService(
                new ConsoleWriter(),
                new TestFoodSpawner(new Vector2Int(1, 1)),
                new ClockWiseMovement(),
                new World2StringConverter()
                );

            // Food generates on (1, 1) so it must be eaten on 2nd tick
            worldSimulator.Tick();
            worldSimulator.Tick();
            Assert.AreEqual(worldSimulator.Foods.Count, 1);
            Assert.Greater(worldSimulator.Worms.ElementAt(0).Health, 10);
        }

        public class TestFoodSpawner : IFoodGenerator
        {
            private Vector2Int _targetFoodPosition;

            public TestFoodSpawner(Vector2Int initialPostion)
            {
                _targetFoodPosition = initialPostion;
            }

            public Vector2Int GenerateFood(IReadOnlyCollection<WorldObject> forbiddenCells)
            {
                while (Contains(forbiddenCells, _targetFoodPosition))
                {
                    _targetFoodPosition += new Vector2Int(0, 1);
                }

                var result = _targetFoodPosition;

                _targetFoodPosition += new Vector2Int(0, 1);

                return result;
            }

            public bool Contains(IReadOnlyCollection<WorldObject> collection, Vector2Int position)
            {
                foreach (var obj in collection)
                {
                    if (obj.Position == position)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        [Test]
        public void FoodSpawnOnWorm()
        {
            WorldSimulatorService worldSimulator = new WorldSimulatorService(
                new ConsoleWriter(),
                new TestFoodSpawner(new Vector2Int(0, 0)),
                new TestWormAI(0),
                new World2StringConverter()
                );


            worldSimulator.Tick();
            Assert.AreEqual(worldSimulator.Foods.Count, 0);
            Assert.Greater(worldSimulator.Worms.ElementAt(0).Health, 10);
        }

        [Test]
        public void ReproduceInBusyCell()
        {
            WorldSimulatorService worldSimulator = new WorldSimulatorService(
                new ConsoleWriter(),
                new TestFoodSpawner(new Vector2Int(0, 0)),
                new TestWormAI(0),
                new World2StringConverter()
                );

            worldSimulator.Tick();
            worldSimulator.Tick();

            Assert.AreEqual(worldSimulator.Worms.Count, 1);
            // 10 hp spend regardless of reproduce success
            Assert.Less(worldSimulator.Worms.ElementAt(0).Health, 10);
        }

        [Test]
        public void ReproduceInEmptyCell()
        {
            WorldSimulatorService worldSimulator = new WorldSimulatorService(
                new ConsoleWriter(),
                new TestFoodSpawner(new Vector2Int(0, 0)),
                new TestWormAI(1),
                new World2StringConverter()
                );

            worldSimulator.Tick();
            worldSimulator.Tick();

            Assert.AreEqual(worldSimulator.Worms.Count, 2);
            Assert.AreNotEqual(worldSimulator.Worms.ElementAt(0).Name, worldSimulator.Worms.ElementAt(1).Name);
            Assert.Less(worldSimulator.Worms.ElementAt(0).Health, 10);
        }

        [Test]
        public void MoveOnWorm()
        {
            WorldSimulatorService worldSimulator = new WorldSimulatorService(
                new ConsoleWriter(),
                new TestFoodSpawner(new Vector2Int(0, 0)),
                new TestWormAI(1),
                new World2StringConverter()
                );

            worldSimulator.Tick();
            worldSimulator.Tick();

            var wormPosition = worldSimulator.Worms.ElementAt(0).Position;

            worldSimulator.Tick();

            var newWormPosition = worldSimulator.Worms.ElementAt(0).Position;

            Assert.AreEqual(wormPosition, newWormPosition);
        }

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

        [Test]
        public void ClosestFoodMovement()
        {
            WorldSimulatorService worldSimulator = new WorldSimulatorService(
                new ConsoleWriter(),
                new NormalFoodGenerator(),
                new ChaseClosestFood(),
                new World2StringConverter()
                );

            for (int i = 0; i < 100; i++)
            {
                var oldWormPosition = worldSimulator.Worms.ElementAt(0).Position;
                var oldHealth = worldSimulator.Worms.ElementAt(0).Health;

                worldSimulator.Tick();

                if(!worldSimulator.Foods.Any())
                {
                    continue;
                }

                if(oldHealth < worldSimulator.Worms.ElementAt(0).Health)
                {
                    continue;
                }

                var newWormPosition = worldSimulator.Worms.ElementAt(0).Position;
                var foods = worldSimulator.Foods;
                var minDistance = int.MaxValue;

                foreach(var food in foods)
                {
                    minDistance = Math.Min(minDistance, GetDiscreteDistance(oldWormPosition, food.Position));
                }

                var closestFoods = new List<Food>();

                foreach (var food in foods)
                {
                    var distance = GetDiscreteDistance(oldWormPosition, food.Position);

                    if(distance == minDistance)
                    {
                        closestFoods.Add(food);
                    }
                }

                bool success = false;

                foreach (var food in closestFoods)
                {
                    if(GetDiscreteDistance(newWormPosition, food.Position) < minDistance)
                    {
                        success = true;
                        break;
                    }
                }

                if(!success)
                {
                    Assert.Fail();
                }

            }

            int GetDiscreteDistance(Vector2Int a, Vector2Int b)
            {
                var direction = b - a;
                return Math.Abs(direction.X) + Math.Abs(direction.Y);
            }
        }
    }
}