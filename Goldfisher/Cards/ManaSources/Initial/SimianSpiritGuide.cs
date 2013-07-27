namespace Goldfisher.Cards
{
	public class SimianSpiritGuide : ManaSource
	{
		public SimianSpiritGuide()
		{
			Name = "Simian Spirit Guide";
			Type = CardType.InitialMana;
			Color = Color.Red;
		    Cost = Manacost.None;
			Produces = new Manapool("R");

		    Priority = 0.5m;        //Early
		}

		public override bool CanCast(BoardState boardState)
		{
			return true;		//Always
		}

		public override void Resolve(BoardState boardState)
		{
			//Put on stack and pay cost
			boardState.Hand.Remove(this);

			//Get effect
		    boardState.Manapool.Add(Produces);

			//Finish resolution
			boardState.Exile.Add(this);

			//Log
			boardState.Log(Usage.Exile, this);
		}
	}
}
