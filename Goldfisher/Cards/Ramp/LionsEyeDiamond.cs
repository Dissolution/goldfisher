namespace Goldfisher
{
	public class LionsEyeDiamond : Card
	{
		public LionsEyeDiamond()
		{
			this.Name = "Lion's Eye Diamond";
			this.Type = CardType.Ramp;

			this.Priority = 1.5m;		//Later
		}

		public override bool CanCast(BoardState boardState)
		{
			//Yes, we can always cast this. It's mana we're using only for wincons, thought.
			return true;
		}

		public override void Resolve(BoardState boardState)
		{
			//Put on stack and pay cost
			boardState.Hand.Remove(this);

			//Get effect
			boardState.LedMana += 3;
			boardState.Storm += 1;

			//Finish resolution
			boardState.Graveyard.Add(this);

			//Log
			boardState.Log(Usage.Cast, this);
		}
	}
}
