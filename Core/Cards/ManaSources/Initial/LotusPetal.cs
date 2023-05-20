namespace Jay.Goldfisher.Cards.ManaSources.Initial;

public sealed class LotusPetal : ManaSource
{
    public LotusPetal()
    {
        Name = "Lotus Petal";
        ShortName = "Petal";
        Type = CardRole.InitialMana;
        Color = Color.None;
        Cost = ManaValue.None;
        Produces = new ManaPool("*");

        Priority = 1.1m;
    }

    public override bool CanCast(BoardState boardState)
    {
        return true;
    }

    public override bool Resolve(BoardState boardState)
    {
        //Put on stack and pay cost
        boardState.Hand.Remove(this);

        //Get effect
        boardState.Manapool.Add(Produces);
        boardState.Storm += 1;

        //Finish Resolution
        boardState.Graveyard.Add(this);

        //Log
        boardState.Log(Usage.Cast, this);
        return true;
    }
}