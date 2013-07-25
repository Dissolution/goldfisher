namespace Goldfisher
{
	public class ElvishSpiritGuide : Card
	{
		public ElvishSpiritGuide()
		{
			this.Name = "Elvish Spirit Guide";
			this.Type = CardType.InitialMana;
			this.Color = Color.Green;
			
			this.Priority = .5m;		//Very early
			this.EndMana = 1;
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
			boardState.Manapool.Add("G");
		
			//Finish resolution
			boardState.Exile.Add(this);

			//Log
			boardState.Log(Usage.Exile, this);
		}
	}
}
