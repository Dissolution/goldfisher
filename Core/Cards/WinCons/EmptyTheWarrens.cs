namespace Jay.Goldfisher.Cards.WinCons;

public class EmptyTheWarrens : Card
{
    public EmptyTheWarrens()
    {
        Name = "Empty the Warrens";
        ShortName = "Empty";
        Type = CardRole.WinCon;
        Color = Color.Red;
        Cost = new ManaValue("3R");

        Priority = 3.0m;		//WinCon
    }


    public override bool CanCast(BoardState boardState)
    {
        //Override if we have Belcher and 7 mana
        if (boardState.Hand.Any(c => c.Name == "Goblin Charbelcher") &&
            boardState.Manapool.Total >= 4 &&
            boardState.Manapool.Total + boardState.LedMana >= 7)
            return false;

        //Override if we have Burninc Wish and 6 mana (better storm)
        if (boardState.Hand.Any(c => c.Name == "Burning Wish") &&
            boardState.Manapool.CanPay(new ManaValue("1R")) &&
            boardState.Manapool.Total + boardState.LedMana >= 6)
            return false;

        //Otherwise, can we pay for it?
        return boardState.Manapool.CanPay(Cost);
    }

    public override bool Resolve(BoardState boardState)
    {
        //Pay costs, put on stack.
        boardState.Manapool.Pay(Cost);
        boardState.Hand.Remove(this);

        //Resolve
        boardState.Storm += 1;
        boardState.Graveyard.Add(this);
        boardState.WinConditionType = WinConditionType.Empty;

        //Log
        boardState.Log(Usage.Cast, this, "Storm {0}".FormatWith(boardState.Storm));
        return true;
    }
}