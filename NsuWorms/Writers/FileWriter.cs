using System;
using System.IO;

namespace NsuWorms.Writers
{
    public class FileWriter : IWriter, IDisposable
    {
        private StreamWriter _streamWriter;

        public FileWriter(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            _streamWriter = File.CreateText(path);
        }

        public void Dispose()
        {
            _streamWriter.Dispose();
        }

        public void WriteLine(string line)
        {
            _streamWriter.WriteLine(line);
        }
    }
}
