using NsuWorms.World;
using NsuWormsWorldBehaviourGenerator.Database;
using NsuWormsWorldBehaviourGenerator.MathUtils;
using System;
using System.Collections.Generic;

namespace NsuWormsWorldBehaviourGenerator.Core.Generation
{
    public class NormalRandomGenerator : IGenerator
    {
        private List<Vector2Int> _foods = new List<Vector2Int>();
        private string _behaviourName;

        public NormalRandomGenerator(string behaviourName)
        {
            _behaviourName = behaviourName;
        }

        public Behaviour GenerateBehaviour()
        {
            FillList();
            return new Behaviour() { Id = _behaviourName, Points = GetString() };
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
