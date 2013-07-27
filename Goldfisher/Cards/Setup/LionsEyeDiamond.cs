namespace Goldfisher.Cards
{
	public class LionsEyeDiamond : Card
	{
		public LionsEyeDiamond()
		{
			Name = "Lion's Eye Diamond";
			Type = CardType.Ramp;
            Color = Color.None;
		    Cost = Manacost.None;

			Priority = 1.9m;		//Last of ramp
		}

		public override bool CanCast(BoardState boardState)
		{
			//Yes, we can always cast this.
			return true;
		}

		public override void Resolve(BoardState boardState)
		{
            //Pay costs, put on stack.
            boardState.Manapool.Pay(Cost);
            boardState.Hand.Remove(this);

            //Resolve
		    boardState.LedMana += 3;
            boardState.Storm += 1;
            boardState.Graveyard.Add(this);

            //Log
            boardState.Log(Usage.Cast, this);
		}
	}
}
