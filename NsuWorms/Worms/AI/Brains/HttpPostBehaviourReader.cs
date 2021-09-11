using NsuWorms.MathUtils;
using NsuWorms.Models;
using NsuWorms.World;
using NsuWorms.Worms.AI.Behaviours;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace NsuWorms.Worms.AI.Brains
{
    public class HttpPostBehaviourReader : IWormBrain
    {
        private string _urlBase;

        public HttpPostBehaviourReader(string ip, string port)
        {
            _urlBase = $"https://{ip}:{port}/";
        }

        public BehaviourEntity RequestBehaviour(Worm target, WorldSimulatorService world)
        {
            var model = CreateWorldModel(world);
            var json = JsonSerializer.Serialize(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{_urlBase}BehaviourGenerator/{target.Name}/getAction";

            using (var client = new HttpClient())
            {
                var task = client.PostAsync(url, data);
                task.Wait(1000);
                var response = task.Result;
                string result = response.Content.ReadAsStringAsync().Result;
                var behaviour = JsonSerializer.Deserialize<BehaviourModel>(result);
                return CreateBehaviourFromModel(behaviour);
            }
        }

        private WorldModel CreateWorldModel(WorldSimulatorService world)
        {
            var worms = new WormModel[world.Worms.Count];
            var foods = new FoodModel[world.Foods.Count];

            for (int i = 0; i < worms.Length; i++)
            {
                worms[i] = new WormModel()
                {
                    Name = world.Worms.ElementAt(i).Name,
                    LifeStrength = world.Worms.ElementAt(i).Health,
                    Position = new PositionModel()
                    {
                        X = world.Worms.ElementAt(i).Position.X,
                        Y = world.Worms.ElementAt(i).Position.Y
                    }
                };
            }

            for (int i = 0; i < foods.Length; i++)
            {
                foods[i] = new FoodModel()
                {
                    ExpiresIn = world.Foods.ElementAt(i).LifeTime,
                    Position = new PositionModel()
                    {
                        X = world.Foods.ElementAt(i).Position.X,
                        Y = world.Foods.ElementAt(i).Position.Y
                    }
                };
            }

            return new WorldModel() { Worms = worms, Food = foods };
        }

        private BehaviourEntity CreateBehaviourFromModel(BehaviourModel model)
        {
            Direction? direction;

            switch(model.direciton)
            {
                case "Up": direction = Direction.Up;
                    break;
                case "Down": direction = Direction.Down;
                    break;
                case "Right": direction = Direction.Right;
                    break;
                case "Left": direction = Direction.Left;
                    break;
                default: direction = null;
                    break;
            }

            if(direction == null)
            {
                return new NullBehaviour();
            }

            if(model.split)
            {
                return new ReproduceBehaviour((Direction)direction);
            }

            return new MoveInDirectionBehaviour((Direction)direction);
        }
    }
}
