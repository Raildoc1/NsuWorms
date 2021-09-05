using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NsuWorms.Database;
using NsuWormsWorldBehaviourGenerator.Core.Generation;
using NsuWormsWorldBehaviourGenerator.Database.DatabaseWriter;
using System.Configuration;

namespace NsuWormsWorldBehaviourGenerator.Core
{
    public class EntryPoint
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<DatabaseBehaviourDataProviderService>();
                    services.AddScoped<IGenerator>(ctx => { return new NormalRandomGenerator(args[0]); });
                    services.AddScoped<IDatabaseBehaviourWriter, SqlBehaviourWriter>();
                    services.AddDbContextPool<BehavioursDbContext>(options => options.UseSqlServer(ConfigurationManager.ConnectionStrings["localWindowsDatabase"].ConnectionString));
                });
        }
    }
}
