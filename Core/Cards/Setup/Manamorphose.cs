using Jay.Goldfisher.Cards.Base;
using Jay.Goldfisher.Enumerations;
using Jay.Goldfisher.Extensions;
using Jay.Goldfisher.Types;

namespace Jay.Goldfisher.Cards.Setup;

public class Manamorphose : ManaSource
{
    private readonly Manacost _secondCost;

    public Manamorphose()
    {
        Name = "Manamorphose";
        ShortName = "MM";
        Type = CardType.Draw;
        Color = Color.Red | Color.Green;
			
        Cost = new Manacost("1R");
        _secondCost = new Manacost("1G");

        Produces = new Manapool("**");

        Priority = 2.1m;
    }

    public override bool CanCast(BoardState boardState)
    {
        return (boardState.Manapool.CanPay(Cost) ||
            boardState.Manapool.CanPay(_secondCost));
    }

    public override bool Resolve(BoardState boardState)
    {
        //Pay costs, put on stack.
        boardState.Manapool.Pay(boardState.Manapool.CanPay(Cost) ? Cost : _secondCost);
        boardState.Hand.Remove(this);

        //Resolve
        boardState.Manapool.Add(Produces);
        var drawn = boardState.DrawCards(1).First();
        boardState.Storm += 1;
        boardState.Graveyard.Add(this);

        //Log
        boardState.Log(Usage.Cast, this, "Draw {0}".FormatWith(drawn));
        return true;
    }
}