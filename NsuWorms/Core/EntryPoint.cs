using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NsuWorms.Database;
using NsuWorms.World;
using NsuWorms.World.FoodGeneration;
using NsuWorms.Worms.AI;
using NsuWorms.Worms.AI.Brains;
using NsuWorms.Worms.NamesGeneration;
using NsuWorms.Writers;

namespace NsuWorms.Core
{
    public sealed class EntryPoint
    {
        private static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<WorldSimulatorService>();
                    services.AddSingleton<IWriter>(ctx => { return new FileWriter("output.txt"); });
                    services.AddSingleton<INamesGenerator>(ctx => { return new SimpleUniqueNamesGenerator("Ivan"); });
                    services.AddSingleton<IFoodGenerator, PreloadedFoodGenerator>();
                    services.AddSingleton<IFoodDataLoader, DatabaseFoodLoader>();
                    services.AddSingleton<IDatabaseFoodReader>(ctx => { return new DatabaseFoodReader(args[0]); });
                    services.AddSingleton<IWorld2StringConverter, World2StringConverter>();
                    services.AddSingleton<IWormBrain, ChaseClosestFood>();
                    services.AddSingleton<BehavioursDbContext>();
                });
        }
    }
}
