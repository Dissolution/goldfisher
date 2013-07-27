namespace Goldfisher.Cards
{
	public class ElvishSpiritGuide : ManaSource
	{
		public ElvishSpiritGuide()
		{
			Name = "Elvish Spirit Guide";
			Type = CardType.InitialMana;
			Color = Color.Green;
		    Cost = Manacost.None;
            Produces = new Manapool("G");

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
