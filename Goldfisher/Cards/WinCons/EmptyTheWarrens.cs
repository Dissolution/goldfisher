using System.Linq;

namespace Goldfisher.Cards
{
	public class EmptyTheWarrens : Card
	{
		public EmptyTheWarrens()
		{
			Name = "Empty the Warrens";
			Type = CardType.WinCon;
			Color = Color.Red;
			Cost = new Manacost("3R");

			Priority = 2.0m;		//WinCon
		}


		public override bool CanCast(BoardState boardState)
		{
			//Override if we have Burning Wish + enough mana (better storm)
			if ((boardState.Hand.Any(c => c.Name == "Burning Wish" &&
			                             boardState.Manapool.CanPay(new Manacost("1R")) &&
			                             boardState.Manapool.Total + boardState.LedMana >= 6) ||
				boardState.Hand.Any(c => c.Name == "Goblin Charbelcher") &&
				boardState.Manapool.CanPay(new Manacost("4")) &&
				boardState.Manapool.Total + boardState.LedMana >= 7))
				return false;

			return boardState.Manapool.CanPay(Cost);
		}

		public override void Resolve(BoardState boardState)
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
		}
	}
}
