using System.Linq;

namespace Goldfisher
{
	public class GoblinCharbelcher : Card
	{
		public GoblinCharbelcher()
		{
			this.Name = "Goblin Charbelcher";
			this.Type = CardType.WinCon;
			this.Cost = new Manacost("4");

			this.Priority = 2.2m;		//WinCon
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
			boardState.WinConditionType = WinConditionType.Belcher;
			boardState.Storm += 1;

			//Finish resolution
			boardState.Battlefield.Add(this);

			//Log
			boardState.Log(Usage.Cast, this);
		}
	}
}
