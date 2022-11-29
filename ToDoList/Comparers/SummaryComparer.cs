using System;
using System.Collections.Generic;

namespace ToDo.Comparers
{
    internal sealed class SummaryComparer : IComparer<IToDo>
    {
        public int Compare(IToDo x, IToDo y)
        {
            if (x is null || y is null)
            {
                return -1;
            }
            else
            {
                return StringComparer.CurrentCultureIgnoreCase.Compare(x.Summary.ToString(), y.Summary.ToString());
            }
        }

        public override bool Equals(object obj) => obj is not null && obj is SummaryComparer;

        public override int GetHashCode() => typeof(SummaryComparer).GetHashCode();

        public override string ToString() => nameof(SummaryComparer);
    }
}
