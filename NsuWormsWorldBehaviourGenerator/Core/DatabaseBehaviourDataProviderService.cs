using Microsoft.Extensions.Hosting;
using NsuWormsWorldBehaviourGenerator.Core.Generation;
using NsuWormsWorldBehaviourGenerator.Database.DatabaseWriter;
using System.Threading;
using System.Threading.Tasks;

namespace NsuWormsWorldBehaviourGenerator.Core
{
    public sealed class DatabaseBehaviourDataProviderService : IHostedService
    {
        private readonly IGenerator _generator;
        private readonly IDatabaseBehaviourWriter _databaseWriter;

        public DatabaseBehaviourDataProviderService(IGenerator generator, IDatabaseBehaviourWriter databaseWriter)
        {
            _generator = generator;
            _databaseWriter = databaseWriter;
        }

        public void Execute()
        {
            _databaseWriter.WriteBehaviour(_generator.GenerateBehaviour());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Execute();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
