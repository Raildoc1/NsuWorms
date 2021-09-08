namespace NsuWorms.Worms.NamesGeneration
{
    public class SimpleUniqueNamesGenerator : INamesGenerator
    {
        private string _baseName;
        private int _number = 0;

        public SimpleUniqueNamesGenerator(string baseName = "Worm")
        {
            _baseName = baseName;
        }

        public string Next()
        {
            var result = _number == 0 ? _baseName : _baseName + _number.ToString();
            _number++;
            return result;
        }
    }
}
