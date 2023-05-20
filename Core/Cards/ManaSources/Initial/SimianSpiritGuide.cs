namespace Jay.Goldfisher.Cards.ManaSources.Initial;

public class SimianSpiritGuide : ManaSource
{
    public SimianSpiritGuide()
    {
        Name = "Simian Spirit Guide";
        ShortName = "SSG";
        Type = CardRole.InitialMana;
        Color = Color.Red;
        Cost = ManaValue.None;
        Produces = new ManaPool("R");

        Priority = 1.0m;
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

        //Finish resolution
        boardState.Exile.Add(this);

        //Log
        boardState.Log(Usage.Exile, this);
        return true;
    }
}