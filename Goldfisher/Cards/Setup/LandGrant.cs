namespace Goldfisher.Cards
{
	public class LandGrant : Card
	{

		public LandGrant()
		{
			Name = "Land Grant";
			Type = CardType.InitialMana;
			Color = Color.Green;
		    Cost = Manacost.None;

			Priority = .3m;		//Initial Mana
		}


		public override bool CanCast(BoardState boardState)
		{
			return true;
		}

		public override void Resolve(BoardState boardState)
		{
            //Pay costs, put on stack.
            boardState.Manapool.Pay(Cost);
            boardState.Hand.Remove(this);

            //Resolve
            boardState.Storm += 1;
            boardState.Graveyard.Add(this);
            //Try to find Taiga
		    var taiga = boardState.Library.Find(c => c.Name == "Taiga");
            if (taiga != null)
            {
                //Put taiga in hand.
                boardState.Library.Remove(taiga);
                boardState.Hand.Add(taiga);

                //Log
                boardState.Log(Usage.Cast, this, "Taiga -> Hand");
            }
            else
            {
                //Log
                boardState.Log(Usage.Cast, this, "Fail to find");
            }
		}
	}
}
