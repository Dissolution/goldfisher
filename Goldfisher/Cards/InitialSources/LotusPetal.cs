namespace Goldfisher
{
	public sealed class LotusPetal : Card
	{
		public LotusPetal()
		{
			this.Name = "Lotus Petal";
			this.Type = CardType.InitialMana;

			this.Priority = .5m;		//Early
			this.EndMana = 1;
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
			boardState.Manapool.Add("*");		//Any
			boardState.Storm += 1;

			//Finish Resolution
			boardState.Graveyard.Add(this);

			//Log
			boardState.Log(Usage.Cast, this);
		}
	}
}
