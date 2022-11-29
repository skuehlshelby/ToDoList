using System;
using System.Collections.Generic;

namespace Application.Sorting
{
    internal interface ISortMethod
    {
        void Sort<TContainer, TItem>(TContainer container) where TContainer : IIndexable<int, TItem>, ICountable where TItem : IComparable<TItem>;

        void Sort<TContainer, TItem>(TContainer container, IComparer<TItem> comparer) where TContainer : IIndexable<int, TItem>, ICountable;
    }
}
