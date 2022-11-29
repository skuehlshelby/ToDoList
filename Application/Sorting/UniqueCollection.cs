using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Application.Sorting
{
    internal sealed class UniqueCollection<T> : IIndexable<int, T>, ICountable, ISortable<T>, ICollection<T>, INotifyCollectionChanged
    {
        private readonly ArrayWrapper<T> values = new();

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public int Count => values.Count;

        public bool IsReadOnly => values.IsReadOnly;

        T IIndexable<int, T>.this[int index]
        {
            get => values[index];
            set
            {
                if (!values[index].Equals(value))
                {
                    CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, values[index], value, index));
                    values[index] = value;
                }
            }
        }

        void ICollection<T>.Add(T item) => _ = Add(item);

        public bool Add(T item)
        {
            if (!Contains(item))
            {
                values.Add(item);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, values.Count - 1));
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Clear()
        {
            if (0 < values.Count)
            {
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, Array.Empty<T>(), values));
                values.Clear();
            }
        }

        public bool Contains(T item)
        {
            return values.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            values.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            if (values.Remove(item))
            {
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Sort(ISortMethod sortMethod, IComparer<T> comparer)
        {
            sortMethod.Sort(this, comparer);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)values).GetEnumerator();
        }
    }
}
