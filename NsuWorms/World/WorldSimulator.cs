using NsuWorms.MathUtils;
using NsuWorms.Worms;
using NsuWorms.Worms.AI;
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

        public WorldSimulator(IWriter writer)
        {
            _worms.Add(new Worm(Vector2Int.Zero, new ClockWiseMovement(), "Ivan"));
            _writer = writer;
            WriteData();
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
                position = new Vector2Int(random.NextNormal(), random.NextNormal());
            } while (!IsCellFree(position));
            _foods.Add(new Food(position));
        }

        private void UpdateWorms()
        {
            foreach (var worm in _worms)
            {
                worm.ApplyBehaviour(worm.RequestBehaviour(this));
            }
        }

        private void TryEatFood()
        {
            foreach (var worm in _worms)
            {
                var food = GetFoodAt(worm.Position);

                if(food != null)
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
                if(!first)
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

            if(food != null)
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
