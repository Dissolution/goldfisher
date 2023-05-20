namespace Jay.Goldfisher.Cards.ManaSources.Ramp;

public class PyreticRitual : ManaSource
{
    public PyreticRitual()
    {
        Name = "Pyretic Ritual";
        ShortName = "Pyretic";
        Type = CardRole.Ramp;
        Color = Color.Red;
        Cost = new ManaValue("1R");
        Produces = new ManaPool("RRR");

        Priority = 2.3m;
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