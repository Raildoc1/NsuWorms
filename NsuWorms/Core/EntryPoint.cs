﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NsuWorms.World;
using NsuWorms.Worms.AI;
using NsuWorms.Worms.AI.Brains;
using NsuWorms.Writers;

namespace NsuWorms
{
    public class EntryPoint
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
                    services.AddScoped<IFoodGenerator>(ctx => { return new SimpleFoodGenerator(1); });
                    services.AddScoped<IWorld2StringConverter, World2StringConverter>();
                    services.AddScoped<IWormBrain, ChaseClosestFood>();
                });
        }
    }
}
