using System.Linq;

namespace Goldfisher
{
	public class BurningWish : Card
	{
		public BurningWish()
		{
			this.Name = "Burning Wish";
			this.Type = CardType.WinCon;
			this.Color = Color.Red;
			this.Cost = new Manacost("1R");

			this.Priority = 2.1m;		//WinCon
		}


		public override bool CanCast(BoardState boardState)
		{
			return (boardState.Manapool.CanPay(Cost) &&
			        boardState.Manapool.Total + boardState.LedMana >= 6);
		}

		public override void Resolve(BoardState boardState)
		{
			//Put on stack and pay cost
			boardState.Hand.Remove(this);
			boardState.Manapool.Pay(Cost);

			//Get effect
			//boardState.WinConditionType = WinConditionType.Empty;
			boardState.Storm += 1;

			//LED?
			if (boardState.LedMana > 0)
			{
				//Add LED mana to pool (in response)
				boardState.Graveyard.AddRange(boardState.Hand);
				boardState.Hand.Clear();
				boardState.Manapool.Add(boardState.LedMana, Color.Any);
				boardState.LedMana = 0;
			}

			boardState.Hand.Add(new EmptyTheWarrens());

			//Finish resolution
			boardState.Exile.Add(this);

			//Log
			boardState.Log(Usage.Cast, this, "Put Empty the Warrens in Hand");
		}
	}
}
