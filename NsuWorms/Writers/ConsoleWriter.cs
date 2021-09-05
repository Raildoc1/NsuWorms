using System;

namespace NsuWorms.Writers
{
    public sealed class ConsoleWriter : IWriter
    {
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
