namespace Goldfisher.Cards
{
	public class SeethingSong : ManaSource
	{
		public SeethingSong()
		{
			Name = "Seething Song";
		    ShortName = "Song";
			Type = CardType.Ramp;
			Color = Color.Red;
			Cost = new Manacost("2R");
            Produces = new Manapool("RRRRR");

		    Priority = 2.4m;
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
