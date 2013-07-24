using System;
using System.Collections.Generic;
using System.Linq;
using Jay;

namespace Goldfisher
{
	public class ChromeMox : Card
	{
		public ChromeMox()
		{
			this.Name = "Chrome Mox";
			this.Type = CardType.InitialMana;

			this.Priority = .2m;
			this.EndMana = 1;
		}

		public override bool CanCast(BoardState boardState)
		{
			//If we have a land grant + no taiga, let it resolve first
			if (boardState.Hand.Any(c => c.Name == "Land Grant") &&
			    boardState.Battlefield.All(c => c.Name != "Taiga"))
				return false;

			//Otherwise, verify we have a single colored card in hand
			if (boardState.GoalType == WinConditionType.Belcher)
				return boardState.Hand.Any(c => c.Color != Color.None);
			else
			{
				return boardState.Hand.Any(c => c.Color != Color.None && !c.Name.EqualsAny("Burning Wish", "Empty the Warrens"));
			}
		}

		public override void Resolve(BoardState boardState)
		{
			//Check our win type. 
			var imprints = new List<string>();
			if (boardState.GoalType == WinConditionType.Belcher)
			{
				//If we want to belch, we are okay with imprinting non-manasource cards (storm doesn't matter)
				imprints.AddRange("Empty the Warrens", "Land Grant", "Burning Wish",
				                  "Elvish Spirit Guide", "Simian Spirit Guide",
				                  "Tinder Wall", "Desperate Ritual", "Pyretic Ritual",
				                  "Manamorphose", "Rite of Flame", "Seething Song");
			}
			else
			{
				//Storm count is more important
				//But we will eat cards that we will never cast
				var eCount = boardState.Hand.Count(c => c.Name == "Empty the Warrens");
				if (eCount >= 1)
					imprints.Add("Burning Wish");
				if (eCount > 1)
					imprints.Add("Empty the Warrens");

				var bCount = boardState.Hand.Count(c => c.Name == "Burning Wish");
				if (bCount >= 1)
					imprints.Add("Empty the Warrens");
				if (bCount > 1)
					imprints.Add("Burning Wish");

				imprints.AddRange("Elvish Spirit Guide", "Simian Spirit Guide",
									"Land Grant", "Tinder Wall", "Desperate Ritual", "Pyretic Ritual",
								  "Manamorphose", "Rite of Flame", "Seething Song");
			}
			
			//Check for proper imprint
			foreach (var imprint in imprints)
			{
				var found = boardState.Hand.Find(c => c.Name == imprint);
				if (found != null)
				{
					//Put on stack and pay cost
					boardState.Hand.Remove(this);
					boardState.Hand.Remove(found);

					//Get effect
					boardState.Manapool.Add(found.Color);

					//Finish resolution
					boardState.Battlefield.Add(this);
					boardState.Exile.Add(found);

					//Log
					boardState.Log(Usage.Cast, this, "Imprinting {0}".FormatWith(found.Name));
					return;
				}
			}

			//Couldn't cast!?
			throw new Exception("WTF?!");
		}
	}
}
