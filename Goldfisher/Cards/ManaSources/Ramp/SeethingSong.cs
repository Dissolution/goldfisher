namespace Goldfisher.Cards
{
	public class SeethingSong : ManaSource
	{
		public SeethingSong()
		{
			Name = "Seething Song";
			Type = CardType.Ramp;
			Color = Color.Red;
			Cost = new Manacost("2R");
            Produces = new Manapool("RRRRR");

            Priority = 1.5m;		//After other ramp, to not eat Green
		}

		public override bool CanCast(BoardState boardState)
		{
			return boardState.Manapool.CanPay(Cost);
		}

		public override void Resolve(BoardState boardState)
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
		}
	}
}
