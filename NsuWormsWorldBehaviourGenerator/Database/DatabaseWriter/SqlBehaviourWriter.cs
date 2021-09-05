using NsuWorms.Database;
using NsuWorms.World;

namespace NsuWormsWorldBehaviourGenerator.Database.DatabaseWriter
{
    public sealed class SqlBehaviourWriter : IDatabaseBehaviourWriter
    {
        private readonly BehavioursDbContext _context;

        public SqlBehaviourWriter(BehavioursDbContext context)
        {
            _context = context;
        }

        public void WriteBehaviour(Behaviour behaviour)
        {
            _context.Behaviours.Add(behaviour);
            _context.SaveChanges();
        }
    }
}
