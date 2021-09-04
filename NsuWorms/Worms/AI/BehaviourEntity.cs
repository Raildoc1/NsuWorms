namespace NsuWorms.Worms.AI
{
    public enum BehaviourType
    {
        Null,
        ChangeDirection,
        Reproduce
    }

    public abstract class BehaviourEntity
    {
        public abstract BehaviourType GetBehaviourType();
    }
}
