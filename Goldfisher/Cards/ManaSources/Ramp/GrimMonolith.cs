namespace Goldfisher.Cards
{
	public class GrimMonolith : ManaSource
	{
		public GrimMonolith()
		{
			Name = "Grim Monolith";
		    ShortName = "Grim";
			Type = CardType.Ramp;
            Color = Color.None;
			Cost = new Manacost("2");
            Produces = new Manapool("3");

		    Priority = 2.6m;
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
            boardState.Manapool.Add(Produces);
            boardState.Storm += 1;
            boardState.Graveyard.Add(this);

            //Log
            boardState.Log(Usage.Cast, this);
			return true;
		}
	}
}
