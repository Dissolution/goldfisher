namespace Goldfisher.Cards
{
	public class TinderWall : ManaSource
	{
        public TinderWall()
		{
			Name = "Tinder Wall";
			Type = CardType.Ramp;
			Color = Color.Green;
			Cost = new Manacost("G");
            Produces = new Manapool("RR");

			Priority = .9m;		//Before other initials, as it uses Green (harder to get)
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
