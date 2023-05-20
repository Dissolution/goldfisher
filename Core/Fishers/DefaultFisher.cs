using System.Diagnostics;
using System.Text;
using Jay.Goldfisher.Cards.Base;
using Jay.Goldfisher.Enumerations;
using Jay.Goldfisher.Extensions;
using Jay.Goldfisher.Types;

namespace Jay.Goldfisher.Fishers;

public class DefaultFisher
{
    public DefaultFisher()
    {
        var library = Decklists.Dissolution();

        //Prime the results
        //Win Con Type, # of that type (damage/tokens), times happened
        var results = new List<Tuple<WinConditionType, int, BoardState>>();

        const int sims = 100000;

        var timer = Stopwatch.StartNew();

        //Number of simulations
        for (var h = 0; h < sims; h++)
        {
            //Create a new boardstate for this simulation.
            var state = new BoardState(library);

#region Testing Specific Hands
            //state.Hand.Clear();
            //state.Hand.AddRange(new LotusPetal(),
            //					new LionsEyeDiamond(),
            //					new ChromeMox(),
            //					new LandGrant(),
            //					new LionsEyeDiamond(),
            //					new BurningWish(),
            //					new GoblinCharbelcher());
            //state.Library.Insert(0, new LionsEyeDiamond());
#endregion

            //Loop until we mull to death or get a 'win'.
            while (state.WinConditionType == WinConditionType.None && state.Hand.Any())
            {
                //Perform simple mulligan check.
                Mulligan(state);

                //If we're out of cards, we failed. Exit
                if (!state.Hand.Any())
                    break;

                //Copy this state so we can go back if we fail.
                var origState = state.Copy();

                //Start casting
                state = Play(state);

                //If we didn't have a wincondition, reset (should've mulliganned)
                if (state.WinConditionType == WinConditionType.None)
                {
                    if (origState.Hand.Count >= 6 && origState.Hand.Any(c => c.Name == "Chrome Mox"))
                    {
                        var play = origState.PlayLog;
                    }

                    //Reset state
                    state = origState;
                    //Force mulligan
                    state.Mulligan("Couldn't fish");
                }
            }

            //Check our win condition
            if (state.WinConditionType == WinConditionType.Empty)
            {
                var tokens = state.Storm * 2;
                state.Log("{0} tokens".FormatWith(tokens));
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
                    state.Log("Can't activate");
                    results.Add(WinConditionType.Belcher, -1, state);
                }
            }
            else
            {
                //Did not win (fizzle)
                state.Log("Fizzle");
                results.Add(WinConditionType.None, 0, state);
            }

            ////I want to look at winconditions without chrome mox logic
            //if (state.Battlefield.Any(c => c.Name == "Chrome Mox"))
            //{
            //	var log = state.PlayLog;
            //}
        }

        timer.Stop();

        var text = new StringBuilder();
        var failures = results.Count(r => r.Item1 == WinConditionType.None);
        text.AppendLine("Failures: {0} ({1}%)".FormatWith(failures, 100*failures/ sims));

        var belches = results.Count(r => r.Item1 == WinConditionType.Belcher);
        text.AppendLine("Belches: {0} ({1}%)".FormatWith(belches, 100*belches/ sims));
        var bDrops = results.Count(r => r.Item1 == WinConditionType.Belcher && r.Item2 == -1);
        text.AppendLine("Drop Only: {0} ({1}%)".FormatWith(bDrops, 100*bDrops/ belches));
        var bFails = results.Count(r => r.Item1 == WinConditionType.Belcher && r.Item2 >= 0 && r.Item2 < 20);
        text.AppendLine("<20 Damage: {0} ({1}%)".FormatWith(bFails, 100*bFails/ belches));
        var bKills = results.Count(r => r.Item1 == WinConditionType.Belcher && r.Item2 >= 20);
        text.AppendLine("20+ Damage: {0} ({1}%)".FormatWith(bKills, 100*bKills/ belches));

        var empties = results.Count(r => r.Item1 == WinConditionType.Empty);
        text.AppendLine("Empties: {0} ({1}%)".FormatWith(empties, 100*empties/ sims));
        for (var t = 2; t <= 36; t += 2)
        {
            var e = results.Count(r => r.Item1 == WinConditionType.Empty && r.Item2 == t);
            text.AppendLine("{0} tokens: {1} ({2}%)".FormatWith(t, e, 100*e/ empties));
        }

        var textStr = text.ToString();
        Console.WriteLine(textStr);
        Debugger.Break();
    }

#region Private Methods

    /// <summary>
    /// Simulate the entire hand's ramp to determine what mana we can end up with.
    /// This WILL NOT take into account Desperate Ritual splice or Rite of Flame multiples.
    /// NOR Chrome Mox.
    /// </summary>
    /// <param name="boardState"></param>
    /// <returns></returns>
    private Manapool DoRamp(BoardState boardState)
    {
        var state = boardState.Copy();
        var skip = 0;

        //Do Land Grant
        if (state.Hand.Any(c => c.Name == "Land Grant") && 
            state.Hand.All(c => c.Name != "Taiga"))
        {
            state.Manapool.Add("*");      //From Taiga
        }

        while (true)
        {
            //Get first manasource by priority
            var card = state.Hand.Where(c => c is ManaSource && c.Name != "Chrome Mox")
                .OrderBy(c => c.Priority)
                .Skip(skip)
                .FirstOrDefault() as ManaSource;
            if (card == null)
            {
                //No more cards to resolve, exit.
                break;
            }

            if (card.CanCast(state))
            {
                card.Resolve(state);
                skip = 0;
            }
            else
            {
                skip += 1;
            }

            //Check if we can't cast anything
            if (skip >= state.Hand.Count)
                break;
        }

        return state.Manapool;
    }

    /// <summary>
    /// Perform necessary mulligans
    /// </summary>
    /// <param name="state"></param>
    private void Mulligan(BoardState state)
    {
        while (state.Hand.Any())
        {
            //Check 1 - Does the hand have a win condition?
            if (state.Hand.All(h => h.Type != CardType.WinCon))
            {
                state.Mulligan("No win cons");
                continue;
            }

            ////Check 2 - Does the hand have enough mana to ramp to it's win condition?
            //var addmana = DoRamp(state).Total;
            //var ledmana = state.Hand.Count(c => c.Name == "Lion's Eye Diamond")*3;
            //if ((state.Hand.Any(c => c.Name == "Goblin Charbelcher") && addmana >= 4) ||
            //    (state.Hand.Any(c => c.Name == "Burning Wish") && addmana >= 2 && addmana + ledmana >= 6) ||
            //    (state.Hand.Any(c => c.Name == "Empty the Warrens") && addmana >= 4))
            //{
            //    //This hand is okay
            //}
            //else
            //{
            //    //Mull
            //    state.Mulligan();
            //    continue;
            //}

            //Checked out
            break;
        }
    }

    /// <summary>
    /// Play this hand.
    /// </summary>
    /// <param name="boardState"></param>
    /// <returns></returns>
    private BoardState Play(BoardState boardState)
    {
        var skip = 0;

        while (true)
        {
            //Get first by prority
            var card = boardState.Hand
                .OrderBy(h => h.Priority)
                .Skip(skip)
                .FirstOrDefault();
            if (card == null)
                break;

            if (card.CanCast(boardState))
            {
                if (card.Resolve(boardState))
                {
                    skip = 0;
                }
                else
                {
                    skip += 1; //Skip this card for now, we can't resolve it.
                }
            }
            else
            {
                skip += 1;
            }

            //Check if we can't cast anything
            if (skip >= boardState.Hand.Count)
                break;

            //Check if we've hit our win condition
            if (boardState.WinConditionType != WinConditionType.None)
                break;
        }

        //Done
        return boardState;
    }
#endregion
}