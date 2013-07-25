using System.Linq;

namespace Goldfisher
{
	public class Manamorphose : Card
	{
		private readonly Manacost _secondCost;

		public Manamorphose()
		{
			this.Name = "Manamorphose";
			this.Type = CardType.Draw;
			this.Color = Color.Red | Color.Green;
			this.Cost = new Manacost("1R");
			_secondCost = new Manacost("1G");

			this.Priority = 1.1m;		//After tinder wall's G
			this.StartMana = 2;
			this.EndMana = 3;		//Treat draw as +1 mana
		}

		public override bool CanCast(BoardState boardState)
		{
			return (boardState.Manapool.CanPay(Cost) ||
			        boardState.Manapool.CanPay(_secondCost));
		}

		public override void Resolve(BoardState boardState)
		{
			//Put on stack and pay costs
			boardState.Hand.Remove(this);
			boardState.Manapool.Pay(boardState.Manapool.CanPay(Cost) ? Cost : _secondCost);

			//Get effect
			boardState.Manapool.Add("**");		//Two of Any
			boardState.Storm += 1;
			var cards = boardState.DrawCards(1);

			//Finish resolution
			boardState.Graveyard.Add(this);

			//Log
			boardState.Log(Usage.Cast, this, "Draw {0}".FormatWith(cards.First()));
		}
	}
}
