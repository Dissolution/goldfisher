using System.Linq;
using Jay;

namespace Goldfisher
{
	public class EmptyTheWarrens : Card
	{
		public EmptyTheWarrens()
		{
			this.Name = "Empty the Warrens";
			this.Type = CardType.WinCon;
			this.Color = Color.Red;
			this.Cost = new Manacost("3R");

			this.Priority = 2.0m;		//WinCon
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
			//Put on stack and pay costs
			boardState.Hand.Remove(this);
			boardState.Manapool.Pay(Cost);

			//Get effect
			boardState.WinConditionType = WinConditionType.Empty;
			boardState.Storm += 1;

			//Finish resolution
			boardState.Graveyard.Add(this);

			//Log
			boardState.Log(Usage.Cast, this, "Storm {0}".FormatWith(boardState.Storm));
		}
	}
}
