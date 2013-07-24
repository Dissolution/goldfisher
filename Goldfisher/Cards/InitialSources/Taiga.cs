namespace Goldfisher
{
	public class Taiga : Card
	{
		public Taiga()
		{
			this.Name = "Taiga";
			this.Type = CardType.InitialMana;

			this.Priority = .1m;		//Very early
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
			boardState.Manapool.Add("*");		//Technically not Any

			//Finish Resolution
			boardState.Battlefield.Add(this);

			//Log
			boardState.Log(Usage.Play, this);
		}
	}
}
