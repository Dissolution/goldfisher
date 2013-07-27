namespace Goldfisher.Cards
{
	public class GrimMonolith : ManaSource
	{
		public GrimMonolith()
		{
			Name = "Grim Monolith";
			Type = CardType.Ramp;
            Color = Color.None;
			Cost = new Manacost("2");
            Produces = new Manapool("3");

			Priority = 1.75m;		//After other ramps (don't want to eat up mana for colorless)
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
