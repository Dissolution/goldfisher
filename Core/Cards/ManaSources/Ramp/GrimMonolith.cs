namespace Jay.Goldfisher.Cards.ManaSources.Ramp;

public class GrimMonolith : ManaSource
{
    public GrimMonolith()
    {
        Name = "Grim Monolith";
        ShortName = "Grim";
        Type = CardRole.Ramp;
        Color = Color.None;
        Cost = new ManaValue("2");
        Produces = new ManaPool("3");

        Priority = 2.6m;
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