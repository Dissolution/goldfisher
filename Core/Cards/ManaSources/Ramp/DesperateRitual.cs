namespace Jay.Goldfisher.Cards.ManaSources.Ramp;

public class DesperateRitual : ManaSource
{
    public DesperateRitual()
    {
        Name = "Desperate Ritual";
        ShortName = "Desperate";
        Type = CardRole.Ramp;
        Color = Color.Red;
        Cost = new ManaValue("1R");
        Produces = new ManaPool("RRR");

        Priority = 2.5m;
    }

    public override bool CanCast(BoardState boardState)
    {
        return boardState.Manapool.CanPay(Cost);
    }

    public override bool Resolve(BoardState boardState)
    {
        //Check if we have another Desperate Ritual and 2RR in pool
        if (boardState.Hand.Count(c => c.Name == Name) >= 2 &&
            boardState.Manapool.CanPay(new ManaValue("2RR")))
        {
            //Splice the other onto this!

            //Pay costs, put on stack
            boardState.Manapool.Pay(new ManaValue("2RR"));
            boardState.Hand.Remove(this);       //Leave the other

            //Resolve
            boardState.Manapool.Add(new ManaPool("RRRRRR"));
            boardState.Storm += 1;
            boardState.Graveyard.Add(this);

            //Log
            boardState.Log(Usage.Cast, this, "Spliced");
        }
        else
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
        }
        return true;
    }
}