using System;
using System.Collections.Generic;

namespace Application.Sorting
{
    internal interface ISelfSorting
    {
        void Sort();
    }

    internal interface ISortable
    {
        void Sort(ISortMethod sortMethod);
    }

    internal interface ISortable<T>
    {
        void Sort(ISortMethod sortMethod, IComparer<T> comparer);
    }
}
