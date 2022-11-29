using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Application
{
    internal sealed class SortableObservableSet<T> : IReadOnlySet<T>, ISet<T>, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private readonly ISet<T> values = new HashSet<T>();

        public int Count => values.Count;

        public bool IsReadOnly => values.IsReadOnly;

        public bool Add(T item)
        {
            if (values.Add(item))
            {
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
                return true;
            }
            else
            {
                return false;
            }
        }

        void ICollection<T>.Add(T item)
        {
            _ = Add(item);
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

        public void Clear()
        {
            values.Clear();
        }

        public bool Contains(T item)
        {
            return values.Contains(item);
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            values.ExceptWith(other);
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            values.IntersectWith(other);
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return values.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return values.IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return values.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return values.IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return values.Overlaps(other);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return values.SetEquals(other);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            values.SymmetricExceptWith(other);
        }

        public void UnionWith(IEnumerable<T> other)
        {
            values.UnionWith(other);
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            values.CopyTo(array, arrayIndex);
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
