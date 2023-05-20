namespace Jay.Goldfisher;

[Flags]
public enum Color
{
    None = 0,
    White = 1 << 0,
    Blue = 1 << 1,
    Black = 1 << 2,
    Red = 1 << 3,
    Green = 1 << 4,

    Any = White | Blue | Black | Red | Green,
}

public static class ColorExtensions
{
    public static int FlagCount(this Color color)
    {
        int count = 0;
        if ((color & Color.White) != Color.None)
            count++;
        if ((color & Color.Blue) != Color.None)
            count++;
        if ((color & Color.Black) != Color.None)
            count++;
        if ((color & Color.Red) != Color.None)
            count++;
        if ((color & Color.Green) != Color.None)
            count++;
        return count;
    }

    public static bool IsGold(this Color color)
    {
        return color.FlagCount() > 1;
    }
}