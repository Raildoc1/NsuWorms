using NsuWorms.World;
using NsuWorms.Writers;

namespace NsuWorms
{
    class EntryPoint
    {
        private static void Main(string[] args)
        {
            using (var fileWrite = new FileWriter("output.txt"))
            {
                var simulator = new WorldSimulator(fileWrite);

                for (int i = 0; i < 100; i++)
                {
                    simulator.Tick();
                }
            }
        }
    }
}
