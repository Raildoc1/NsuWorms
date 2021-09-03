using NsuWorms.World;

namespace NsuWorms.Worms.AI
{
    public interface IWormBrain
    {
        BehaviourEntity RequestBehaviour(Worm target, WorldSimulator world);
    }
}
