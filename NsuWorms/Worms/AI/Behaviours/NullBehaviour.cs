namespace NsuWorms.Worms.AI.Behaviours
{
    public sealed class NullBehaviour : BehaviourEntity
    {
        public override BehaviourType GetBehaviourType()
        {
            return BehaviourType.Null;
        }
    }
}
