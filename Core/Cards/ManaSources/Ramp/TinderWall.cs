using Jay.Goldfisher.Cards.Base;
using Jay.Goldfisher.Enumerations;
using Jay.Goldfisher.Types;

namespace Jay.Goldfisher.Cards.ManaSources.Ramp;

public class TinderWall : ManaSource
{
    public TinderWall()
    {
        Name = "Tinder Wall";
        ShortName = "Tinder";
        Type = CardType.Ramp;
        Color = Color.Green;
        Cost = new Manacost("G");
        Produces = new Manapool("RR");

        Priority = 2.0m;
    }


    public override bool CanCast(BoardState boardState)
    {
        return boardState.Manapool.CanPay(Cost);
    }

    public override bool Resolve(BoardState boardState)
    {
        //Pay costs, put on stack.
        boardState.Manapool.Pay(Cost);
        boardState.Hand.Remove(this);

        //Resolve
        boardState.Manapool.Add(Produces);
        boardState.Storm += 1;
        boardState.Graveyard.Add(this);

        //Log
        boardState.Log(Usage.Cast, this);
        return true;
    }
}