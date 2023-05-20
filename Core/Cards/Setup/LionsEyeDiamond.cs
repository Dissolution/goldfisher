using Jay.Goldfisher.Cards.Base;
using Jay.Goldfisher.Enumerations;
using Jay.Goldfisher.Types;

namespace Jay.Goldfisher.Cards.Setup;

public class LionsEyeDiamond : Card
{
    public LionsEyeDiamond()
    {
        Name = "Lion's Eye Diamond";
        ShortName = "LED";
        Type = CardType.Ramp;
        Color = Color.None;
        Cost = Manacost.None;

        Priority = 0.5m;
    }

    public override bool CanCast(BoardState boardState)
    {
        //Yes, we can always cast this.
        return true;
    }

    public override bool Resolve(BoardState boardState)
    {
        //Pay costs, put on stack.
        boardState.Manapool.Pay(Cost);
        boardState.Hand.Remove(this);

        //Resolve
        boardState.LedMana += 3;
        boardState.Storm += 1;
        boardState.Graveyard.Add(this);

        //Log
        boardState.Log(Usage.Cast, this);
        return true;
    }
}