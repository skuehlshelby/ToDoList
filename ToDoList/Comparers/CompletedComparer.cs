using System.Collections.Generic;

namespace ToDo.Comparers
{
    internal sealed class CompletedComparer : IComparer<IToDo>
    {
        public int Compare(IToDo x, IToDo y)
        {
            if (x is null || y is null)
            {
                return -1;
            }
            else if (x.Completed == y.Completed)
            {
                return 0;
            }
            else
            {
                return x.Completed ? 1 : -1;
            }
        }

        public override bool Equals(object obj) => obj is not null && obj is CompletedComparer;

        public override int GetHashCode() => typeof(CompletedComparer).GetHashCode();

        public override string ToString() => nameof(CompletedComparer);
    }
}
