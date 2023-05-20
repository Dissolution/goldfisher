namespace Jay.Goldfisher.Cards.ManaSources.Initial;

public class ChromeMox : ManaSource
{
    public ChromeMox()
    {
        Name = "Chrome Mox";
        ShortName = "Mox";
        Type = CardRole.InitialMana;
        Color = Color.None;
        Cost = ManaValue.None;
        Produces = ManaPool.Empty;      //Treat as if it produces nothing

        Priority = 0.3m;     //Very early
    }

    private void Imprint(BoardState state, Card card)
    {
        //Put on stack and pay cost
        state.Hand.Remove(this);
        state.Hand.Remove(card);

        //Get effect
        state.Manapool.Add(card.Color);

        //Finish resolution
        state.Battlefield.Add(this);
        state.Exile.Add(card);

        //Log
        state.Log(Usage.Cast, this, "Imprinting {0}".FormatWith(card.Name));
    }


    public override bool CanCast(BoardState boardState)
    {
        //If we have a land grant + no taiga, let it resolve first
        if (boardState.Hand.Any(c => c.Name == "Land Grant") &&
            boardState.Battlefield.All(c => c.Name != "Taiga"))
            return false;

        //Otherwise, verify we have a single colored card in hand
        return boardState.Hand.Any(c => c.Color != Color.None);
    }

    public override bool Resolve(BoardState boardState)
    {
        //We might not have to resolve ourselves, depending on what's in hand

        //Check for multiples of WinCons
        var cards = boardState.Hand.Where(c => c.Name == "Empty the Warrens").ToList();
        if (cards.Count > 1)
        {
            Imprint(boardState, cards.First());
            return true;
        }
        cards = boardState.Hand.Where(c => c.Name == "Burning Wish").ToList();
        if (cards.Count > 1)
        {
            Imprint(boardState, cards.First());
            return true;
        }
        cards = boardState.Hand.Where(c => c.Name.EqualsAny("Burning Wish", "Empty the Warrens")).ToList();
        if (cards.Count > 1)
        {
            //Check mana (assuming I imprint onto R
            if (boardState.Manapool.Total >= 1 &&
                boardState.Manapool.Total + boardState.LedMana >= 5)
            {
                Imprint(boardState, cards.First(c => c.Name == "Empty the Warrens"));
                return true;
            }

            if (boardState.Manapool.Total >= 3)
            {
                Imprint(boardState, cards.First(c => c.Name == "Burning Wish"));
                return true;
            }

            //Wait!
            return false;
        }

			
        //Check for 'free' imprints
        var card = boardState.Hand
            .FirstOrDefault(c => c.Name.EqualsAny("Land Grant",
                "Simian Spirit Guide",
                "Elvish Spirit Guide",
                "Pyretic Ritual",
                "Desperate Ritual"));
        if (card != null)
        {
            Imprint(boardState, card);
            return true;
        }

        //Check for conditional imprints
        if (boardState.Hand.Any(c => c.Name == "Tinder Wall") &&
            boardState.Hand.Any(c => c.Name != "Tinder Wall" && c.Cost.Green > 0))
        {
            //Imprint Tinder Wall
            card = boardState.Hand.First(c => c.Name == "Tinder Wall");
            Imprint(boardState, card);
            return true;
        }

        //Don't imprint!?
        return false;


        //Couldn't cast!?
        throw new Exception("WTF?!");
    }
}