using System.Linq;

namespace Goldfisher
{
	public class GitaxianProbe : Card
	{
		public GitaxianProbe()
		{
			this.Name = "Gitaxian Probe";
			this.Type = CardType.Draw;
			this.Color = Color.Blue;

			this.Priority = 0.10m;		//First!!!
			this.StartMana = 2;
			this.EndMana = 3;		//Treat draw as +1 mana
		}

		public override bool CanCast(BoardState boardState)
		{
			//If we have taiga in play, safe to cast.
			if (boardState.Battlefield.Any(c => c.Name == "Taiga"))
				return true;

			//If we have any land grants in hand, we want to resolve at least one
			if (boardState.Hand.Any(c => c.Name == "Land Grant"))
				return false;

			return true;
		}

		public override void Resolve(BoardState boardState)
		{
			//Put on stack and pay cost
			boardState.Hand.Remove(this);

			//Get effect
			var cards = boardState.DrawCards(1);
			boardState.Storm += 1;

			//Finish resolution
			boardState.Graveyard.Add(this);

			//Log
			boardState.Log(Usage.Cast, this, "Draw {0}".FormatWith(cards.First()));
		}
	}
}
