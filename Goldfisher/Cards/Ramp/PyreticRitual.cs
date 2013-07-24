using System.Linq;

namespace Goldfisher
{
	public class PyreticRitual : Card
	{
		public PyreticRitual()
		{
			this.Name = "Pyretic Ritual";
			this.Type = CardType.Ramp;
			this.Color = Color.Red;
			this.Cost = new Manacost("1R");

			this.Priority = 1.0m;		//After initials
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
			boardState.Manapool.Add("RRR");
			boardState.Storm += 1;

			//Finish Resolution
			boardState.Graveyard.Add(this);
			
			//Log
			boardState.Log(Usage.Cast, this);
		}
	}
}
