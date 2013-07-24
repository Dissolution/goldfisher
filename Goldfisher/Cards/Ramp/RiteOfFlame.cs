using System.Linq;
using Jay;

namespace Goldfisher
{
	public class RiteOfFlame : Card
	{

		public RiteOfFlame()
		{
			this.Name = "Rite of Flame";
			this.Type = CardType.Ramp;
			this.Color = Color.Red;
			this.Cost = new Manacost("R");

			this.Priority = 1.0m;		//Setup mana
			this.StartMana = 1;
			this.EndMana = 2;
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
			var addRed = 'R'.Repeat(2 + boardState.Graveyard.Count(gy => gy.Name == Name));
			boardState.Manapool.Add(addRed);
			boardState.Storm += 1;

			//Finish resolution
			boardState.Graveyard.Add(this);

			//Log
			boardState.Log(Usage.Cast, this);
		}
	}
}
