
namespace Application.Sorting
{
    internal interface IIndexable<in TIndex, TValue>
    {
        TValue this[TIndex index] { get; set; }
    }

    internal interface IIndexAccessable<in TIndex, out TValue>
    {
        TValue this[TIndex index] { get; }
    }

    internal static class IndexableExtensions
    {
        public static void Swap<T>(this IIndexable<int, T> indexable, int leftIndex, int rightIndex)
        {
            if (leftIndex != rightIndex)
            {
                (indexable[leftIndex], indexable[rightIndex]) = (indexable[rightIndex], indexable[leftIndex]);
            }
        }

        public static void Swap<T, K>(this T indexable, int leftIndex, int rightIndex) where T : IIndexable<int, K>, ICountable
        {
            if (leftIndex != rightIndex && indexable.IsInBounds(leftIndex) && indexable.IsInBounds(rightIndex))
            {
                (indexable[leftIndex], indexable[rightIndex]) = (indexable[rightIndex], indexable[leftIndex]);
            }
        }
    }
}
