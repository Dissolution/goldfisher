using System.Linq;

namespace Goldfisher.Cards
{
	public class StreetWraith : Card
	{
		public StreetWraith()
		{
			Name = "Street Wraith";
		    ShortName = "SW";
			Type = CardType.Draw;
			Color = Color.Black;
		    Cost = Manacost.None;

			Priority = 0.2m;
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

		public override bool Resolve(BoardState boardState)
		{
            //Pay costs, put on stack.
            boardState.Manapool.Pay(Cost);
            boardState.Hand.Remove(this);

            //Resolve
            var drawn = boardState.DrawCards(1).First();
            boardState.Graveyard.Add(this);

            //Log
            boardState.Log(Usage.Cycle, this, "Draw {0}".FormatWith(drawn));
			return true;
		}
	}
}
