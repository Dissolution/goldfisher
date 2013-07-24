using System.Linq;

namespace Goldfisher
{
	public class TinderWall : Card
	{

		public TinderWall()
		{
			this.Name = "Tinder Wall";
			this.Type = CardType.Ramp;
			this.Color = Color.Green;
			this.Cost = new Manacost("G");

			this.Priority = .9m;		//Before other initials, as it uses Green (harder to get)
			this.StartMana = 1;
			this.EndMana = 2;
		}


		public override bool CanCast(BoardState boardState)
		{
			return boardState.Manapool.CanPay(Cost);
		}

		public override void Resolve(BoardState boardState)
		{
			//Put on stack and pay cost
			boardState.Hand.Remove(this);
			boardState.Manapool.Pay(Cost);

			//Get effect
			boardState.Manapool.Add("RR");
			boardState.Storm += 1;

			//Finish resolution
			boardState.Graveyard.Add(this);

			//Log
			boardState.Log(Usage.Cast, this);
		}
	}
}
