using Jay.Goldfisher.Types;

namespace Jay.Goldfisher.Cards.Base;

public abstract class ManaSource : Card
{
#region Properties
    public Manapool Produces { get; protected set; }
#endregion

#region Constructors
    protected ManaSource()
    {
        //Produces nothing
        this.Produces = Manapool.Empty;
    }
#endregion
}