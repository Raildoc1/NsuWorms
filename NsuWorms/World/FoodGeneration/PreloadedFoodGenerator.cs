using NsuWorms.MathUtils;
using System;
using System.Collections.Generic;

namespace NsuWorms.World.FoodGeneration
{
    public sealed class PreloadedFoodGenerator : IFoodGenerator
    {
        private readonly IFoodDataLoader _foodDataLoader;

        private List<Vector2Int> _foods = new List<Vector2Int>();
        private int _currentMove = 0;

        public PreloadedFoodGenerator(IFoodDataLoader foodDataLoader)
        {
            _foodDataLoader = foodDataLoader;

            try
            {
                Fill(_foodDataLoader.Load());
            }
            catch (Exception)
            {
                _foods.Clear();
                Console.WriteLine("Failed to load behaviour!");
                _foods.Add(Vector2Int.Zero);
            }
        }

        public Vector2Int GenerateFood(IReadOnlyCollection<WorldObject> _)
        {
            var move = _currentMove;
            _currentMove = (_currentMove + 1) % _foods.Count;
            return _foods[move];
        }

        private void Fill(string data)
        {
            try
            {
                _foods.Clear();
                string[] points = data.Split(',');
                foreach (var point in points)
                {
                    string[] coordinates = point.Split('.');
                    _foods.Add(new Vector2Int(
                            int.Parse(coordinates[0]),
                            int.Parse(coordinates[1])
                            ));
                }
            }
            catch (Exception)
            {
                _foods.Clear();
                Console.WriteLine("Failed to parse behaviour!");
                _foods.Add(Vector2Int.Zero);
            }
        }
    }
}
