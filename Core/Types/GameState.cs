namespace Jay.Goldfisher.Types;

public class Deck : Stack<Card>
{

}

public class Pile : Stack<Card>
{

}

public class Bag : List<Card>
{
    public bool Any<TCard>()
        where TCard : Card
    {
        return this.Any(static card => card is TCard);
    }
}


public class GameStateCards
{

    public Deck Library { get; } = new();
    public Bag Hand { get; } = new();
    public Pile Graveyard { get; } = new();
    public Bag Exile { get; } = new();
    public Bag Battlefield { get; } = new();

}

public class GameState : GameStateCards
{
    private readonly List<string> _log = new();


    public int StormCount { get; set; } = 0;
    public ManaPool Pool { get; set; } = ManaPool.Empty;
    public int LEDMana { get; set; } = 0;

    public WinConditionType WinConditionType { get; set; } = WinConditionType.None;

    public GameState(IEnumerable<Card> initialLibrary)
    {
        foreach (var card in initialLibrary)
        {
            this.Library.Push(card);
        }
    }

}
