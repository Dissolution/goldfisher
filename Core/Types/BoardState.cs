namespace Jay.Goldfisher.Types;




public class BoardState
{
    public List<string> PlayLog { get; set; }

    public List<Card> Library { get; set; } 
    public List<Card> Hand { get; set; }
    public List<Card> Graveyard { get; set; }
    public List<Card> Exile { get; set; }
    public List<Card> Battlefield { get; set; }

    public int Storm { get; set; }

    public ManaPool Manapool { get; set; }
    public int LedMana { get; set; }

    public WinConditionType WinConditionType { get; set; }

#region Constructors
    public BoardState(List<Card> library)
    {
        Library = library.Copy();
        Reset();

        Shuffle();
        DrawCards(7);
        Log("Opening Hand: " + string.Join(", ", Hand.OrderBy(c => c.Cost.Total).Select(c => c.ShortName)));
    }
#endregion

#region Private Methods
    private void Reset()
    {
        Hand = new List<Card>();
        Graveyard = new List<Card>();
        Exile = new List<Card>();
        Battlefield = new List<Card>();
        PlayLog = new List<string>();
        Manapool = new ManaPool();
        LedMana = 0;
        Storm = 0;

        WinConditionType = WinConditionType.None;
    }
#endregion

#region Public Methods
    public void Log(Usage usage, Card card, string effect)
    {
        PlayLog.Add("({0}): {1} {2} - {3}".FormatWith(Manapool, usage, card.Name, effect));
    }
    public void Log(Usage usage, Card card)
    {
        PlayLog.Add("({0}): {1} {2}".FormatWith(Manapool, usage, card.Name));
    }
    public void Log(string text)
    {
        PlayLog.Add(text);
    }

    public List<Card> DrawCards(int number)
    {
        if (number < 0)
            throw new ArgumentOutOfRangeException("number");

        var cards = Library.Take(number).ToList().Copy();
        Library.RemoveRange(0, number);
        Hand.AddRange(cards);
        return cards;
    }

    public void Shuffle()
    {
        Library.Randomize();
    }

    public void Mulligan(string reason)
    {
        Log(reason);
        var cardsInHand = Hand.Count;
        var originalHand = Hand.Copy();
        Library.AddRange(Hand);
        Hand.Clear();
        Shuffle();
        DrawCards(cardsInHand - 1);
        Log("Mulled to: " + string.Join(", ", Hand.OrderBy(c => c.Cost.Total).Select(c => c.ShortName)));
    }

    public BoardState Copy()
    {
        var state = new BoardState(Library);
        state.Hand = Hand.Copy();
        state.Graveyard = Graveyard.Copy();
        state.Exile = Exile.Copy();
        state.Battlefield = Battlefield.Copy();
        state.Storm = Storm;
        state.Manapool = Manapool.Copy();
        state.LedMana = LedMana;
        state.WinConditionType = WinConditionType;
        state.PlayLog = PlayLog.Copy();
        return state;
    }
#endregion
}