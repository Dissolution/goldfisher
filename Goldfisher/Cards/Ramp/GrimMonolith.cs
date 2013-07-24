using System.Linq;

namespace Goldfisher
{
	public class GrimMonolith : Card
	{
		public GrimMonolith()
		{
			this.Name = "Grim Monolith";
			this.Type = CardType.Ramp;
			this.Cost = new Manacost("2");

			this.Priority = 1.01m;		//After initials
			this.StartMana = 2;
			this.EndMana = 3;
		}

		public override bool CanCast(BoardState boardState)
		{
			return boardState.Manapool.CanPay(Cost);
		}

		public override void Resolve(BoardState boardState)
		{
			//Put on stack and pay costs
			boardState.Hand.Remove(this);
			boardState.Manapool.Pay(Cost);

			//Get effect
			boardState.Manapool.Add("3");
			boardState.Storm += 1;

			//Finish Resolution
			boardState.Graveyard.Add(this);

			//Log
			boardState.Log(Usage.Cast, this);
		}
	}
}
