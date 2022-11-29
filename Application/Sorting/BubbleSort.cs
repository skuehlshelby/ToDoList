using System;
using System.Collections.Generic;

namespace Application.Sorting
{
    internal class BubbleSort : ISortMethod
    {
        public void Sort<TContainer, TItem>(TContainer container) where TContainer : IIndexable<int, TItem>, ICountable where TItem : IComparable<TItem>
        {
            bool swapped;

            do
            {
                swapped = false;

                for (int i = 1; i < container.Count; i++)
                {
                    if (container[i - 1].CompareTo(container[i]) > 0)
                    {
                        container.Swap(i - 1, i);
                        swapped = true;
                    }
                }
            } while (swapped);
        }

        public void Sort<TContainer, TItem>(TContainer container, IComparer<TItem> comparer) where TContainer : IIndexable<int, TItem>, ICountable
        {
            bool swapped;

            do
            {
                swapped = false;

                for (int i = 1; i < container.Count; i++)
                {
                    if (comparer.Compare(container[i - 1], container[i]) > 0)
                    {
                        container.Swap(i - 1, i);
                        swapped = true;
                    }
                }
            } while (swapped);
        }
    }
}
