namespace Goldfisher.Cards
{
	public class ElvishSpiritGuide : ManaSource
	{
		public ElvishSpiritGuide()
		{
			Name = "Elvish Spirit Guide";
		    ShortName = "ESG";
			Type = CardType.InitialMana;
			Color = Color.Green;
		    Cost = Manacost.None;
            Produces = new Manapool("G");

		    Priority = 1.0m;
		}

		public override bool CanCast(BoardState boardState)
		{
			return true;		//Always
		}

		public override bool Resolve(BoardState boardState)
		{
			//Put on stack and pay cost
			boardState.Hand.Remove(this);

			//Get effect
		    boardState.Manapool.Add(Produces);
		
			//Finish resolution
			boardState.Exile.Add(this);

			//Log
			boardState.Log(Usage.Exile, this);
			return true;
		}
	}
}
