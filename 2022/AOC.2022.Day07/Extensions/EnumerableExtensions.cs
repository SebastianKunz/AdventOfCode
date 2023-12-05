namespace AdventOfCode.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> SelectRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
    {
        foreach (T parent in source)
        {
            yield return parent;

            IEnumerable<T> children = selector(parent);
            foreach (T child in SelectRecursive(children, selector))
            {
                yield return child;
            }
        }
    }
}