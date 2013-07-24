using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Jay;

namespace Goldfisher
{
	public class DefaultFisher
	{
		public DefaultFisher()
		{
			var library = Decklist.MyList();

			//Prime the results
			//Win Con Type, # of that type (damage/tokens), times happened
			var results = new List<Tuple<WinConditionType, int, BoardState>>();

			const int sims = 100000;

			var timer = Stopwatch.StartNew();

			//Number of simulations
			for (var h = 0; h < sims; h++)
			{
				//Copy the state for this sim
				var state = new BoardState(library);
				state.Library.Randomize();
				state.DrawCards(7);

                state.Hand.Clear();
			    state.Hand.AddRange(new LotusPetal(), new RiteOfFlame(), new RiteOfFlame(), new EmptyTheWarrens(),
			                        new LionsEyeDiamond(), new GoblinCharbelcher());

				Mulligan(state);

			    var hash = JayMath.CreateHash(state.Hand.OrderBy(c => c.Name));

				//Start casting
				state = Play(state);

				//Check our win condition
				if (state.WinConditionType == WinConditionType.Empty)
				{
					var tokens = state.Storm * 2;
					results.Add(WinConditionType.Empty, tokens, state);
				}
				else if (state.WinConditionType == WinConditionType.Belcher)
				{
					//Can we pay for it's activation?
					if (state.Manapool.Total + state.LedMana >= 3)
					{
						//Check for LED mana
						if (state.LedMana > 0)
						{
							state.Manapool.Add(state.LedMana, Color.Any);
							state.LedMana = 0;
						}
						state.Manapool.Pay(new Manacost("3"));
						
						//Calculate damage
						var index = state.Library.FindIndex(c => c.Name == "Taiga");
						var damage = index == -1 ? state.Library.Count : index*2;

						state.Log(Usage.Activate, state.Battlefield.Find(c => c.Name == "Goblin Charbelcher"), damage.ToString());

						results.Add(WinConditionType.Belcher, damage, state);
					}
					else
					{
						//Drop only
						results.Add(WinConditionType.Belcher, -1, state);
					}
				}
				else
				{
					//Did not win
					results.Add(WinConditionType.None, 0, state);
				}
			}

			timer.Stop();

			var text = new StringBuilder();
			var failures = results.Count(r => r.Item1 == WinConditionType.None);
			text.AppendLine("Failures: {0} ({1}%)".FormatWith(failures, JayMath.Percent(failures, sims)));

			var belches = results.Count(r => r.Item1 == WinConditionType.Belcher);
			text.AppendLine("Belches: {0} ({1}%)".FormatWith(belches, JayMath.Percent(belches, sims)));
			var bDrops = results.Count(r => r.Item1 == WinConditionType.Belcher && r.Item2 == -1);
			text.AppendLine("Drop Only: {0} ({1}%)".FormatWith(bDrops, JayMath.Percent(bDrops, belches)));
			var bFails = results.Count(r => r.Item1 == WinConditionType.Belcher && r.Item2 >= 0 && r.Item2 < 20);
			text.AppendLine("<20 Damage: {0} ({1}%)".FormatWith(bFails, JayMath.Percent(bFails, belches)));
			var bKills = results.Count(r => r.Item1 == WinConditionType.Belcher && r.Item2 >= 20);
			text.AppendLine("20+ Damage: {0} ({1}%)".FormatWith(bKills, JayMath.Percent(bKills, belches)));

			var empties = results.Count(r => r.Item1 == WinConditionType.Empty);
			text.AppendLine("Empties: {0} ({1}%)".FormatWith(empties, JayMath.Percent(empties, sims)));
			for (var t = 2; t <= 36; t += 2)
			{
				var e = results.Count(r => r.Item1 == WinConditionType.Empty && r.Item2 == t);
				text.AppendLine("{0} tokens: {1} ({2}%)".FormatWith(t, e, JayMath.Percent(e, empties)));
			}

			MessageBox.Show(text.ToString());
		}

		#region Private Methods
		private List<Card> LoadBelcher()
		{
			var deck = new List<Card>();
			for (var i = 0; i < 4; i++)
			{
				if (i == 0)
				{
					deck.Add(new Taiga());
				}
				else
				{
					deck.Add(new EmptyTheWarrens());
				}

				deck.Add(new GitaxianProbe());
				deck.Add(new StreetWraith());

				deck.Add(new LotusPetal());
				deck.Add(new SimianSpiritGuide());
				deck.Add(new ElvishSpiritGuide());
				deck.Add(new LandGrant());
				deck.Add(new ChromeMox());

				deck.Add(new Manamorphose());
				//deck.Add(new DesperateRitual());
				deck.Add(new RiteOfFlame());
				//deck.Add(new PyreticRitual());
				deck.Add(new SeethingSong());
				deck.Add(new TinderWall());

				deck.Add(new LionsEyeDiamond());
				deck.Add(new GoblinCharbelcher());
				deck.Add(new BurningWish());
			}

			return deck;
		}

		private void Mulligan(BoardState state)
		{
			while (state.Hand.Any())
			{
				//Check 1 - Does the hand have a win condition?
				if (state.Hand.All(h => h.Type != CardType.WinCon))
				{
					state.Mulligan();
					continue;
				}

				//Check 2 - Does the hand have initial mana to ramp to first mana?
                var addMana = 0;
                var mulliganned = false;
                for (var startMana = 0; startMana <= 3; startMana++)
                {
                    if (addMana < startMana)
                    {
                        state.Mulligan();
                        mulliganned = true;
                        break;
                    }
                
                    addMana += state.Hand.Where(c => c.StartMana == startMana).Sum(c => c.AddMana);
				}

                if (mulliganned)
                {
                    mulliganned = false;
                    continue;
                }

				//Check 3 - Check for enough mana to wincon
				var mana = state.Hand.Sum(c => c.AddMana);
				if ((state.Hand.Any(c => c.Name == "Empty the Warrens") && mana >= 4) ||
				    (state.Hand.Any(c => c.Name == "Goblin Charbelcher") && mana >= 4) ||
				    (state.Hand.Any(c => c.Name == "Burning Wish") &&
				     (mana >= 2) && (mana + (state.Hand.Count(c => c.Name == "Lion's Eye Diamond")*3) >= 6)))
				{
					//Okay!
				}
				else
				{
					state.Mulligan();
					continue;
				}

				break;
			}
		}

		private BoardState Play(BoardState boardState)
		{
			var skip = 0;

			//Try to determine how we're going to win (Belcher > Empty)
			if (boardState.Hand.Contains(new GoblinCharbelcher()))
			{
				boardState.GoalType = WinConditionType.Belcher;
			}
			else
			{
				boardState.GoalType = WinConditionType.Empty;
			}

			while (true)
			{
				//Get first by prority
				var card = boardState.Hand.OrderBy(h => h.Priority).Skip(skip).FirstOrDefault();
				if (card.IsDefault())
					break;

				if (card.CanCast(boardState))
				{
					card.Resolve(boardState);
					skip = 0;
				}
				else
				{
					skip += 1;
				}

				if (skip >= boardState.Hand.Count)
					break;
				if (boardState.WinConditionType != WinConditionType.None)
					break;
			}

			//Done!?
			return boardState;
		}
		#endregion
	}
}
