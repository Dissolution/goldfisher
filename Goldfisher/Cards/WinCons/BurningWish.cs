namespace Goldfisher.Cards
{
	public class BurningWish : Card
	{
		public BurningWish()
		{
			Name = "Burning Wish";
			Type = CardType.WinCon;
			Color = Color.Red;
			Cost = new Manacost("1R");

			Priority = 2.1m;		//WinCon
		}


		public override bool CanCast(BoardState boardState)
		{
			return (boardState.Manapool.CanPay(Cost) &&
			        boardState.Manapool.Total + boardState.LedMana >= 6);
		}

		public override void Resolve(BoardState boardState)
		{
            //Pay costs, put on stack.
            boardState.Manapool.Pay(Cost);
            boardState.Hand.Remove(this);

			//Resolve
            boardState.Storm += 1;

            //Check for LED mana floating, can add to normal pool (hold priority)
            if (boardState.LedMana > 0)
            {
                //Led cost, discard hand
                boardState.Graveyard.AddRange(boardState.Hand);
                boardState.Hand.Clear();

                //Add LED mana to pool
                boardState.Manapool.Add(boardState.LedMana, Color.Any);
                boardState.LedMana = 0;
            }

            //Put Empty in hand
            boardState.Hand.Add(new EmptyTheWarrens());
            boardState.Exile.Add(this);

            //Log
			boardState.Log(Usage.Cast, this, "Empty -> Hand");
		}
	}
}
