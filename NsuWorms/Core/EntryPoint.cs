using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NsuWorms.Database;
using NsuWorms.World;
using NsuWorms.World.FoodGeneration;
using NsuWorms.Worms.AI;
using NsuWorms.Worms.AI.Brains;
using NsuWorms.Writers;
using System.Configuration;

namespace NsuWorms.Core
{
    public sealed class EntryPoint
    {
        private static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<WorldSimulatorService>();
                    services.AddScoped<IWriter>(ctx => { return new FileWriter("output.txt"); });
                    services.AddScoped<IFoodGenerator, PreloadedFoodGenerator>();
                    services.AddScoped<IFoodDataLoader, DatabaseFoodLoader>();
                    services.AddScoped<IDatabaseFoodReader>(ctx => { return new DatabaseFoodReader(args[0]); });
                    services.AddScoped<IWorld2StringConverter, World2StringConverter>();
                    services.AddScoped<IWormBrain, ChaseClosestFood>();
                    services.AddDbContextPool<BehavioursDbContext>(options => options.UseSqlServer(ConfigurationManager.ConnectionStrings["localWindowsDatabase"].ConnectionString));
                });
        }
    }
}
