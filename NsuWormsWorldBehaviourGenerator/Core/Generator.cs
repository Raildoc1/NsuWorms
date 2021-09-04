using NsuWormsWorldBehaviourGenerator.MathUtils;
using System;
using System.Collections.Generic;

namespace NsuWormsWorldBehaviourGenerator.Core
{
    public class Generator
    {
        private List<Vector2Int> _foods = new List<Vector2Int>();

        public string GenerateBehaviour()
        {
            FillList();
            return GetString();
        }

        private void FillList()
        {
            Random random = new Random();

            for (int i = 0; i < 100; i++)
            {
                Vector2Int temp;
                do
                {
                    temp = new Vector2Int(random.NextNormal(0, 5), random.NextNormal(0, 5));
                } while (_foods.Contains(temp));
                _foods.Add(temp);
            }
        }

        private string GetString()
        {
            var result = "";

            for (int i = 0; i < 99; i++)
            {
                result += $"{_foods[i].X}.{_foods[i].Y},";
            }

            result += $"{_foods[99].X}.{_foods[99].Y}";

            return result;
        }
    }
}
