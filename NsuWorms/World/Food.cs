using NsuWorms.MathUtils;

namespace NsuWorms.World
{
    public sealed class Food : WorldObject
    {
        private int _lifeTime;
        public int LifeTime => _lifeTime;

        public Food(Vector2Int initialPosition) : base(initialPosition)
        {
            _lifeTime = 10;
        }

        public void Tick()
        {
            _lifeTime--;
        }
    }
}
