using NsuWormsWorldBehaviourGenerator.Core;

namespace NsuWormsWorldBehaviourGenerator.Database.DatabaseWriter
{
    public interface IDatabaseBehaviourWriter
    {
        void WriteBehaviour(Behaviour behaviour);
    }
}
