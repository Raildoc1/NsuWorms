using NsuWorms.Math;
using NsuWorms.Worms;
using NsuWorms.Worms.AI;
using NsuWorms.Writers;
using System.Collections.Generic;

namespace NsuWorms.World
{
    public class WorldSimulator
    {
        private List<Worm> _worms = new List<Worm>();
        private IWriter _writer;

        public WorldSimulator(IWriter writer)
        {
            _worms.Add(new Worm(Vector2Int.Zero, new ClockWiseMovement(), "Ivan"));
            _writer = writer;
            WriteData();
        }

        public void Tick()
        {
            foreach (var worm in _worms)
            {
                worm.ApplyBehaviour(worm.RequestBehaviour(this));
            }
            WriteData();
        }

        private void WriteData()
        {
            var line = "Worms:[";

            var first = true;

            foreach (var worm in _worms)
            {
                if(!first)
                {
                    first = false;
                    line += ",";
                }
                line += $"{worm.Name}({worm.Position.X},{worm.Position.Y})";
            }

            line += "]";

            _writer.WriteLine(line);
        }
    }
}
