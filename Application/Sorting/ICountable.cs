namespace Application.Sorting
{
    internal interface ICountable
    {
        int Count { get; }
    }

    internal static class CountableExtensions
    {
        public static bool IsInBounds(this ICountable countable, int value)
        {
            return 0 <= value && value < countable.Count;
        }
    }
}
