namespace Jay.Goldfisher.Cards.ManaSources.Initial;

public class Taiga : ManaSource
{
    public Taiga()
    {
        Name = "Taiga";
        ShortName = "Taiga";
        Type = CardRole.InitialMana;
        Color = Color.None;
        Cost = ManaValue.None;
        Produces = new ManaPool("*");       //Technically Red or Green, but we only have two colors, so treat as Any.

        Priority = 0.10m;
    }

    public override bool CanCast(BoardState boardState)
    {
        return true;		//Always
    }

    public override bool Resolve(BoardState boardState)
    {
        //Put on stack and pay cost
        boardState.Hand.Remove(this);

        //Get effect
        boardState.Manapool.Add(Produces);

        //Finish Resolution
        boardState.Battlefield.Add(this);

        //Log
        boardState.Log(Usage.Play, this);
        return true;
    }
}