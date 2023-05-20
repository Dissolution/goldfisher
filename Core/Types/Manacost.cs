using System.Text;

namespace Jay.Goldfisher.Types;

public class Manacost
{
#region Fields
    protected readonly Dictionary<Color, int> _mana = new Dictionary<Color, int>();
#endregion

#region Properties
    public int Colorless
    {
        get { return _mana[Color.None]; }
    }

    public int White
    {
        get { return _mana[Color.White]; }
    }
    public int Blue
    {
        get { return _mana[Color.Blue]; }
    }
    public int Black
    {
        get { return _mana[Color.Black]; }
    }
    public int Red
    {
        get { return _mana[Color.Red]; }
    }
    public int Green
    {
        get { return _mana[Color.Green]; }
    }

    public virtual int Total
    {
        get { return Colorless + White + Blue + Black + Red + Green; }
    }

    public int this[Color color]
    {
        get { return _mana[color]; }
    }
#endregion

#region Static Properties
    public static Manacost None
    {
        get { return new Manacost();}
    }
#endregion

#region Constructors
    /// <summary>
    /// Create a new, empty, mana cost
    /// </summary>
    public Manacost()
    {
        //Setup dictionary
        foreach (var color in Enum.GetValues(typeof(Color)))
        {
            _mana.Add((Color)color, 0);
        }
    }

    /// <summary>
    /// Create a manacost from a text representation
    /// </summary>
    /// <param name="cost"></param>
    public Manacost(string cost)
        : this()
    {
        cost = cost.ToUpper();
        var numbers = new string(cost.Where(char.IsNumber).ToArray());
        if (!string.IsNullOrWhiteSpace(numbers))
            _mana[Color.None] = Convert.ToInt32(numbers);
        _mana[Color.White] = cost.Count(c => c == 'W');
        _mana[Color.Blue] = cost.Count(c => c == 'U');
        _mana[Color.Black] = cost.Count(c => c == 'B');
        _mana[Color.Red] = cost.Count(c => c == 'R');
        _mana[Color.Green] = cost.Count(c => c == 'G');
    }
#endregion

#region Overrides
    public override string ToString()
    {
        var text = new StringBuilder();
        if (Colorless > 0)
            text.Append(Colorless);
        text.Append('W', White);
        text.Append('U', Blue);
        text.Append('B', Black);
        text.Append('R', Red);
        text.Append('G', Green);

        return text.ToString();
    }
#endregion
}