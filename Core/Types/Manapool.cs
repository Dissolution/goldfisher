namespace Jay.Goldfisher.Types;

public class ManaPool : ManaValue
{
#region Fields

    private readonly Color[] _baseColors;
#endregion

#region Properties
    public int Any
    {
        get { return _mana[Color.Any]; }
    }

    public bool IsValid
    {
        get
        {
            return Colorless >= 0 &&
                White >= 0 &&
                Blue >= 0 &&
                Black >= 0 &&
                Red >= 0 &&
                Green >= 0 &&
                Any >= 0;
        }
    }

    public override int Total
    {
        get { return Colorless + White + Blue + Black + Red + Green + Any; }
    }
#endregion

#region Static Properties
    public static ManaPool Empty
    {
        get { return new ManaPool();}
    }
#endregion

#region Constructors
    public ManaPool()
        : base()
    {
        _baseColors = new[] {Color.White, Color.Blue, Color.Black, Color.Red, Color.Green};
    }

    public ManaPool(string mana)
        : base(mana)
    {
        _baseColors = new[] {Color.White, Color.Blue, Color.Black, Color.Red, Color.Green};
        _mana[Color.Any] = mana.Count(c => c == '*');
    }
#endregion

#region Public Methods
    public ManaPool Add(ManaPool manapool)
    {
        _mana[Color.None] += manapool.Colorless;
        _mana[Color.White] += manapool.White;
        _mana[Color.Blue] += manapool.Blue;
        _mana[Color.Black] += manapool.Black;
        _mana[Color.Red] += manapool.Red;
        _mana[Color.Green] += manapool.Green;
        _mana[Color.Any] += manapool.Any;
        return this;
    }

    public ManaPool Add(string mana)
    {
        var pool = new ManaPool(mana);
        return Add(pool);
    }

    public ManaPool Add(int amount, Color color)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException("amount");

        if (amount == 0)
            return this;

        if (color.FlagCount() > 1)
            _mana[Color.Any] += amount;
        else
            _mana[color] += amount;
        return this;
    }

    public ManaPool Add(Color color)
    {
        return Add(1, color);
    }

    public ManaPool Subtract(ManaPool manapool)
    {
        _mana[Color.None] -= manapool.Colorless;
        _mana[Color.White] -= manapool.White;
        _mana[Color.Blue] -= manapool.Blue;
        _mana[Color.Black] -= manapool.Black;
        _mana[Color.Red] -= manapool.Red;
        _mana[Color.Green] -= manapool.Green;
        _mana[Color.Any] -= manapool.Any;
        return this;
    }

    public ManaPool Subtract(string mana)
    {
        var pool = new ManaPool(mana);
        return Subtract(pool);
    }

    public bool CanPay(ManaValue manacost)
    {
        //Check individual colors + any
        return Total >= manacost.Total &&
            White + Any >= manacost.White &&
            Blue + Any >= manacost.Blue &&
            Black + Any >= manacost.Black &&
            Red + Any >= manacost.Red &&
            Green + Any >= manacost.Green;
    }

    public ManaPool Pay(ManaValue manacost)
    {
        //Pay for colors from that color or any
        foreach (var color in _baseColors)
        {
            //Do we have a cost for this color?
            if (manacost[color] == 0)
                continue;

            //If we can pay total, pay it
            if (_mana[color] >= manacost[color])
            {
                _mana[color] -= manacost[color];
            }
            else
            {
                //Pay from this color then from Any
                var remains = manacost[color] - _mana[color];
                _mana[color] = 0;
                if (_mana[Color.Any] < remains)
                    throw new Exception("Cannot pay for {0} in cost.".FormatWith(color));
                _mana[Color.Any] -= remains;
            }
        }

        //Then, pay colorless out of colorless, then mana, then any
        if (_mana[Color.None] >= manacost[Color.None])
        {
            _mana[Color.None] -= manacost[Color.None];
        }
        else
        {
            var remains = manacost[Color.None] - _mana[Color.None];
            _mana[Color.None] = 0;
            //Pay from colors sorted most to least
            foreach (var color in _mana.Where(d => d.Key.EqualsAny(_baseColors))
                .OrderByDescending(d => d.Value)
                .Select(d => d.Key))
            {
                if (_mana[color] >= remains)
                {
                    _mana[color] -= remains;
                    remains = 0;
                    break;
                }
				    
                remains -= _mana[color];
                _mana[color] = 0;
            }

            //If remains, subtract from Any
            if (remains > 0)
            {
                if (_mana[Color.Any] < remains)
                    throw new Exception("Cannot pay for cost.");
                _mana[Color.Any] -= remains;
            }
        }

        return this;
    }

    public ManaPool Copy()
    {
        return new ManaPool(ToString());
    }

#endregion

#region Overrides
    public override string ToString()
    {
        var text = base.ToString();
        text += '*'.Repeat(Any);
        return text;
    }
#endregion
}