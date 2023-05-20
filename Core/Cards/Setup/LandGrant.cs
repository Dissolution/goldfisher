namespace Jay.Goldfisher.Cards.Setup;

public class LandGrant : Card
{

    public LandGrant()
    {
        Name = "Land Grant";
        ShortName = "Grant";
        Type = CardRole.InitialMana;
        Color = Color.Green;
        Cost = ManaValue.None;

        Priority = 0.4m;
    }


    public override bool CanCast(BoardState boardState)
    {
        return true;
    }

    public override bool Resolve(BoardState boardState)
    {
        //Pay costs, put on stack.
        boardState.Manapool.Pay(Cost);
        boardState.Hand.Remove(this);

        //Resolve
        boardState.Storm += 1;
        boardState.Graveyard.Add(this);
        //Try to find Taiga
        var taiga = boardState.Library.Find(c => c.Name == "Taiga");
        if (taiga != null)
        {
            //Put taiga in hand.
            boardState.Library.Remove(taiga);
            boardState.Hand.Add(taiga);

            //Log
            boardState.Log(Usage.Cast, this, "Taiga");
        }
        else
        {
            //Log
            boardState.Log(Usage.Cast, this, "Fail to find");
        }
        return true;
    }
}