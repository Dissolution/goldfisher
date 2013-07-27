namespace Goldfisher.Cards
{
	public sealed class LotusPetal : ManaSource
	{
		public LotusPetal()
		{
			Name = "Lotus Petal";
			Type = CardType.InitialMana;
            Color = Color.None;
		    Cost = Manacost.None;
            Produces = new Manapool("*");

		    Priority = 0.5m;        //Early
		}

		public override bool CanCast(BoardState boardState)
		{
			return true;
		}

		public override void Resolve(BoardState boardState)
		{
			//Put on stack and pay cost
			boardState.Hand.Remove(this);

			//Get effect
			boardState.Manapool.Add(Produces);
			boardState.Storm += 1;

			//Finish Resolution
			boardState.Graveyard.Add(this);

			//Log
			boardState.Log(Usage.Cast, this);
		}
	}
}
