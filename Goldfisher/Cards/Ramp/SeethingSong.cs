using System.Linq;

namespace Goldfisher
{
	public class SeethingSong : Card
	{
		public SeethingSong()
		{
			this.Name = "Seething Song";
			this.Type = CardType.Ramp;
			this.Color = Color.Red;
			this.Cost = new Manacost("2R");

			this.Priority = 1.0m;		//After initials
			this.StartMana = 3;
			this.EndMana = 5;
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
			boardState.Manapool.Add("RRRRR");
			boardState.Storm += 1;

			//Finish resolutions
			boardState.Graveyard.Add(this);

			//Log
			boardState.Log(Usage.Cast, this);
		}
	}
}
