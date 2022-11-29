using System.Collections.Generic;

namespace ToDo.Comparers
{
    internal sealed class DateCreatedComparer : IComparer<IToDo>
    {
        public int Compare(IToDo x, IToDo y)
        {
            if (x is null || y is null)
            {
                return -1;
            }
            else
            {
                return x.Created.CompareTo(y.Created);
            }
        }

        public override bool Equals(object obj) => obj is not null && obj is DateCreatedComparer;

        public override int GetHashCode() => typeof(DateCreatedComparer).GetHashCode();

        public override string ToString() => nameof(DateCreatedComparer);
    }
}
