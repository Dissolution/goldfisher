using System.Text;

namespace Jay.Goldfisher.Extensions;

public static class AllExtensions
{
    private static readonly Random _random = new Random();


    public static string FormatWith(this string str, params object[] args)
    {
        return string.Format(str, args);
    }

    public static List<T> Copy<T>(this List<T> list)
    {
        var array = list.ToArray();
        return array.ToList();
    }

    public static bool EqualsAny<T>(this T obj, params T[] objects)
    {
        if (objects == null)
            throw new ArgumentNullException("objects");
        return objects.Contains(obj);
    }

    public static void AddRange<T>(this List<T> list, params T[] items)
    {
        if (items == null)
            throw new ArgumentNullException("items");
        items.ToList().ForEach(list.Add);
    }

    public static string Repeat(this char c, int times)
    {
        if (times < 0)
            throw new ArgumentOutOfRangeException("times");
        var text = new StringBuilder();
        text.Append(c, times);
        return text.ToString();
    }

    public static void Add<T, U, V>(this List<Tuple<T, U, V>> list, T first, U second, V third)
    {
        var tuple = new Tuple<T, U, V>(first, second, third);
        list.Add(tuple);
    }

    public static List<T> Randomize<T>(this List<T> list)
    {
        //Fisher-Yates Shuffle
        var count = list.Count - 1;
        for (var x = count; x > 1; x--)
        {
            var y = _random.Next(x + 1);
            var value = list[y];
            list[y] = list[x];
            list[x] = value;
        }

        return list;
    }

    public static string AsString<T>(this List<T> list)
    {
        return string.Join(", ", list);
    }
}