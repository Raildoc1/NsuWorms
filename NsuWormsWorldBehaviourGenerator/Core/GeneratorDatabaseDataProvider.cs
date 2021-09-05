using Microsoft.Extensions.Hosting;
using NsuWormsWorldBehaviourGenerator.Core.Generation;
using NsuWormsWorldBehaviourGenerator.Database.DatabaseWriter;
using System.Threading;
using System.Threading.Tasks;

namespace NsuWormsWorldBehaviourGenerator.Core
{
    public sealed class GeneratorDatabaseDataProvider : IHostedService
    {
        private readonly IGenerator _generator;
        private readonly IDatabaseBehaviourWriter _databaseWriter;

        public GeneratorDatabaseDataProvider(IGenerator generator, IDatabaseBehaviourWriter databaseWriter)
        {
            _generator = generator;
            _databaseWriter = databaseWriter;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _databaseWriter.WriteBehaviour(_generator.GenerateBehaviour());
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
