using System.Linq;
using Goldfisher.Cards;

namespace Goldfisher
{
	public class GoblinCharbelcher : Card
	{
		public GoblinCharbelcher()
		{
			Name = "Goblin Charbelcher";
		    ShortName = "Belcher";
			Type = CardType.WinCon;
            Color = Color.None;
			Cost = new Manacost("4");

			Priority = 3.2m;		//WinCon
		}


		public override bool CanCast(BoardState boardState)
		{
			return boardState.Manapool.CanPay(Cost);
		}

		public override bool Resolve(BoardState boardState)
		{
            //Pay costs, put on stack.
            boardState.Manapool.Pay(Cost);
            boardState.Hand.Remove(this);

            //Resolve
            boardState.Storm += 1;
            boardState.Battlefield.Add(this);
            boardState.WinConditionType = WinConditionType.Belcher;

            //Log
            boardState.Log(Usage.Cast, this);
			return true;
		}
	}
}
