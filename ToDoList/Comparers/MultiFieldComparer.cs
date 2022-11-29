using System.Collections.Generic;

namespace ToDo.Comparers
{
    internal sealed class MultiFieldComparer : IComparer<IToDo>
    {
        private readonly ISet<IComparer<IToDo>> comparers;

        public MultiFieldComparer(params IComparer<IToDo>[] comparers)
        {
            this.comparers = new HashSet<IComparer<IToDo>>(comparers);
        }

        public int Compare(IToDo x, IToDo y)
        {
            foreach (var comparer in comparers)
            {
                int result = comparer.Compare(x, y);

                if (result != 0)
                {
                    return result;
                }
            }

            return 0;
        }

        public override string ToString() => nameof(MultiFieldComparer);
    }
}
