namespace Jay.Goldfisher.Cards;

partial class Card :
    IEqualityOperators<Card, Card, bool>,
    ISpanParsable<Card>
{
    public static bool operator ==(Card? left, Card? right) => Card.Equals(left, right);
    public static bool operator !=(Card? left, Card? right) => !Card.Equals(left, right);

    private static readonly HashSet<Card> _allKnownCards = new();

    static Card()
    {
        _allKnownCards = typeof(Card)
            .Assembly
            .GetTypes()
            .Where(static type => type.IsAssignableTo(typeof(Card)) && !type.IsAbstract)
            .SelectWhere<Type, Card>((Type type, out Card card) =>
            {
                object? obj = null;
                try
                {
                    obj = Activator.CreateInstance(type);
                }
                catch { }
                if (obj is Card)
                {
                    card = (Card)obj;
                    return true;
                }
                else
                {
                    card = default!;
                    return false;
                }
            })
            .ToHashSet();
    }

    static bool ISpanParsable<Card>.TryParse(ReadOnlySpan<char> text, IFormatProvider? _, [MaybeNullWhen(false)] out Card card) => TryParse(text, out card);




    public static Card Parse(ReadOnlySpan<char> text, IFormatProvider? _ = null)
    {
        if (TryParse(text, out var card))
            return card;
        throw new ArgumentException($"Could not parse '{text}' to a Card", nameof(text));
    }

    public static bool TryParse(ReadOnlySpan<char> text, [MaybeNullWhen(false)] out Card card)
    {
        foreach (var knownCard in _allKnownCards)
        {
            if (text.Equals(knownCard.Name, StringComparison.OrdinalIgnoreCase))
            {
                card = knownCard;
                return true;
            }
        }
        card = null;
        return false;
    }

    public static Card Parse(string? str, IFormatProvider? _ = null) => Parse(str.AsSpan(), _);
    public static bool TryParse([NotNullWhen(true)] string? str, IFormatProvider? _, [MaybeNullWhen(false)] out Card card) => TryParse(str.AsSpan(), out card);


    public static bool Equals(Card? left, Card? right)
    {
        if (ReferenceEquals(left, right))
            return true;
        if (left is null || right is null)
            return false;
        return string.Equals(left.Name, right.Name);
    }
}


public abstract partial class Card :
    IEquatable<Card>,
    ISpanFormattable, IFormattable
{
    /// <summary>
    /// Card's Name
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// Short/Abbreviated Name
    /// </summary>
    public string ShortName { get; protected set; }

    /// <summary>
    /// Card's Role
    /// </summary>
    public CardRole Type { get; protected set; }

    /// <summary>
    /// Card's color(s)
    /// </summary>
    public Color Color { get; protected set; }

    /// <summary>
    /// Card's mana cost
    /// </summary>
    public ManaValue Cost { get; protected set; }

    /// <summary>
    /// Card's cast priority (lower is earlier)
    /// </summary>
    public decimal Priority { get; protected set; }

    protected Card() { }

    public abstract bool CanCast(BoardState boardState);

    public abstract bool Resolve(BoardState boardState);


    public bool Equals(Card? card)
    {
        return string.Equals(this.Name, card?.Name);
    }
    public override bool Equals(object? obj)
    {
        return obj is Card card && Equals(card);
    }
    public override int GetHashCode()
    {
        return string.GetHashCode(this.Name);
    }

    public bool TryFormat(Span<char> destination, out int charsWritten,
        ReadOnlySpan<char> format = default,
        IFormatProvider? provider = default)
    {
        if (format == "s")
        {
            if (this.ShortName.TryCopyTo(destination))
            {
                charsWritten = this.ShortName.Length;
                return true;
            }

        }
        else
        {
            if (this.Name.TryCopyTo(destination))
            {
                charsWritten = this.Name.Length;
                return true;
            }
        }
        charsWritten = 0;
        return false;
    }

    public string ToString(string? format, IFormatProvider? formatProvider = default)
    {
        if (format == "s")
        {
            return this.ShortName;

        }
        else
        {
            return this.Name;
        }
    }

    public override string ToString()
    {
        return Name;
    }
}