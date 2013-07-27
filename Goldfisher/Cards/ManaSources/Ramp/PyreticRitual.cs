namespace Goldfisher.Cards
{
	public class PyreticRitual : ManaSource
	{
		public PyreticRitual()
		{
			Name = "Pyretic Ritual";
			Type = CardType.Ramp;
			Color = Color.Red;
			Cost = new Manacost("1R");
            Produces = new Manapool("RRR");

			Priority = 1.0m;		//After initials
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
