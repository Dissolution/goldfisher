namespace Goldfisher
{
	public class SimianSpiritGuide : Card
	{
		public SimianSpiritGuide()
		{
			this.Name = "Simian Spirit Guide";
			this.Type = CardType.InitialMana;
			this.Color = Color.Red;

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
			boardState.Manapool.Add("R");

			//Finish resolution
			boardState.Exile.Add(this);

			//Log
			boardState.Log(Usage.Exile, this);
		}
	}
}
