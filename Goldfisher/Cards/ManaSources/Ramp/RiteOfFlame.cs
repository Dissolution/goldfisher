﻿using System.Linq;

namespace Goldfisher.Cards
{
	public class RiteOfFlame : ManaSource
	{
        public RiteOfFlame()
		{
			Name = "Rite of Flame";
			Type = CardType.Ramp;
			Color = Color.Red;
			Cost = new Manacost("R");
            Produces = new Manapool("RR");      //Could be more

            Priority = 1.0m;		//Setup mana
		}


		public override bool CanCast(BoardState boardState)
		{
			return boardState.Manapool.CanPay(Cost);
		}

		public override void Resolve(BoardState boardState)
		{
            //Pay costs, put on stack.
            boardState.Manapool.Pay(Cost);
            boardState.Hand.Remove(this);

            //Resolve
            boardState.Manapool.Add(Produces);
            //Add extra red for other copies in graveyard
            boardState.Manapool.Add(
                boardState.Graveyard.Count(c => c.Name == Name), 
                Color.Red);
            boardState.Storm += 1;
            boardState.Graveyard.Add(this);

            //Log
            boardState.Log(Usage.Cast, this);
		}
	}
}
