namespace Jay.Goldfisher.Extensions;

public delegate bool SelectWhere<TIn, TOut>(TIn input, out TOut ouput);

public static class EnumerableExtensions
{
    public static IEnumerable<TOut> SelectWhere<TIn, TOut>(this IEnumerable<TIn> source, SelectWhere<TIn, TOut> selectWherePredicate)
    {
        foreach (TIn input in source)
        {
            if (selectWherePredicate(input, out var output))
                yield return output;
        }
    }
}
