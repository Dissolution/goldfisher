using System.Linq;

namespace Goldfisher.Cards
{
	public class GitaxianProbe : Card
	{
		public GitaxianProbe()
		{
			Name = "Gitaxian Probe";
			Type = CardType.Draw;
			Color = Color.Blue;
		    Cost = Manacost.None;

			Priority = 0.10m;		//First!!!
		}

		public override bool CanCast(BoardState boardState)
		{
			//If we have taiga in play, safe to cast.
			if (boardState.Battlefield.Any(c => c.Name == "Taiga"))
				return true;

			//If we have any land grants in hand, we want to resolve at least one
            //It will then put a taiga in play and be caught above
			if (boardState.Hand.Any(c => c.Name == "Land Grant"))
				return false;

			return true;
		}

		public override void Resolve(BoardState boardState)
		{
            //Pay costs, put on stack.
            boardState.Manapool.Pay(Cost);
            boardState.Hand.Remove(this);

            //Resolve
		    var drawn = boardState.DrawCards(1).First();
            boardState.Storm += 1;
            boardState.Graveyard.Add(this);

            //Log
		    boardState.Log(Usage.Cast, this, "Draw {0}".FormatWith(drawn));
		}
	}
}
