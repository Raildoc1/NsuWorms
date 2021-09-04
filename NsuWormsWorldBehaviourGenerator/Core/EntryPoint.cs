using NsuWormsWorldBehaviourGenerator.SQL;

namespace NsuWormsWorldBehaviourGenerator.Core
{
    public class EntryPoint
    {
        static void Main(string[] args)
        {
            SqlConnector.WriteToDatabase(args[0], new Generator().GenerateBehaviour());
        }
    }
}
