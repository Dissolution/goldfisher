﻿using System.Linq;
using Goldfisher.Cards;

namespace Goldfisher
{
	public class GoblinCharbelcher : Card
	{
		public GoblinCharbelcher()
		{
			Name = "Goblin Charbelcher";
			Type = CardType.WinCon;
            Color = Color.None;
			Cost = new Manacost("4");

			Priority = 2.2m;		//WinCon
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
            boardState.Storm += 1;
            boardState.Battlefield.Add(this);
            boardState.WinConditionType = WinConditionType.Belcher;

            //Log
            boardState.Log(Usage.Cast, this);
		}
	}
}
