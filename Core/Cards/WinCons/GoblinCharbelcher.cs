namespace Jay.Goldfisher.Cards.WinCons;

public class GoblinCharbelcher : Card
{
    public GoblinCharbelcher()
    {
        Name = "Goblin Charbelcher";
        ShortName = "Belcher";
        Type = CardRole.WinCon;
        Color = Color.None;
        Cost = new ManaValue("4");

        Priority = 3.2m;		//WinCon
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
        boardState.Storm += 1;
        boardState.Battlefield.Add(this);
        boardState.WinConditionType = WinConditionType.Belcher;

        //Log
        boardState.Log(Usage.Cast, this);
        return true;
    }
}