using Microsoft.EntityFrameworkCore;
using NsuWorms.Database;
using NsuWorms.World;
using NsuWorms.World.FoodGeneration;
using NsuWorms.Worms.AI.Brains;
using NsuWorms.Worms.NamesGeneration;
using NsuWorms.Writers;
using NsuWormsWorldBehaviourGenerator.Core;
using NsuWormsWorldBehaviourGenerator.Core.Generation;
using NsuWormsWorldBehaviourGenerator.Database.DatabaseWriter;
using NUnit.Framework;
using System;

namespace NsuWormsNUnitTests
{
    public class SqlTests
    {
        public BehavioursDbContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BehavioursDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;
            return new BehavioursDbContext(options);
        }

        [SetUp]
        public void initDatabase()
        {
            var database = GetMemoryContext();
            if(database.Database.IsInMemory())
            {
                database.Database.EnsureDeleted();
            }
        }

        [Test]
        public void GenerateAndLoadData()
        {
            var sqlBehaviourWriter = new SqlBehaviourWriter(GetMemoryContext());
            var databaseBehaviourDataProviderService 
                = new DatabaseBehaviourDataProviderService(
                    new NormalRandomGenerator("beh-1"), 
                    sqlBehaviourWriter
                    );

            databaseBehaviourDataProviderService.Execute();

            var foodGenerator = new PreloadedFoodGenerator(
                new DatabaseFoodLoader(new DatabaseFoodReader("beh-1"), GetMemoryContext()));

            var simulator = new WorldSimulatorService(
                new ConsoleWriter(),
                foodGenerator,
                new ChaseClosestFood(),
                new World2StringConverter(),
                new SimpleUniqueNamesGenerator("Test")
                );

            simulator.Tick();
            simulator.Tick();
            simulator.Tick();

            // if world behaviour isn't loaded correctly food will always spawn on (0,0) and be eaten immediately
            Assert.Greater(simulator.Foods.Count, 0);

            try
            {
                databaseBehaviourDataProviderService.Execute();
                // trying to generate new behaviour with same name must cause exception
                Assert.Fail();
            } catch(Exception) { }
        }
    }
}
