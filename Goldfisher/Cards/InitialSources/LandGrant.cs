using System.Linq;
using Jay;

namespace Goldfisher
{
	public class LandGrant : Card
	{

		public LandGrant()
		{
			this.Name = "Land Grant";
			this.Type = CardType.InitialMana;
			this.Color = Color.Green;

			this.Priority = .3m;		//Initial Mana
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
			//Try to find taiga in library
			var taiga = boardState.Library.Find(c => c.Name == "Taiga");
			if (taiga != null)
			{
				//Put it into our hand.
				boardState.Library.Remove(taiga);
				boardState.Hand.Add(taiga);		//Will be caught during processing

				//Log
				boardState.Log(Usage.Cast, this, "Put Taiga in Hand");
			}
			else
			{
				//Log
				boardState.Log(Usage.Cast, this, "Fail to Find");
			}

			//Add to storm
			boardState.Storm += 1;
			
			//Finish Resolution
			boardState.Graveyard.Add(this);
		}
	}
}
