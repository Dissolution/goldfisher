namespace Jay.Goldfisher.Cards.ManaSources;

public abstract class ManaSource : Card
{
    public ManaPool Produces { get; protected set; } = ManaPool.Empty;

    protected ManaSource() : base()
    {

    }
}