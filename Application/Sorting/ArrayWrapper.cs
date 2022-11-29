using System;
using System.Collections;
using System.Collections.Generic;

namespace Application.Sorting
{
    internal class ArrayWrapper<T> : IIndexable<int, T>, ICountable, ICollection, ICollection<T>, IEnumerable, IEnumerable<T>, IList, IStructuralComparable, IStructuralEquatable, ICloneable
    {
        private T[] array;
        private readonly int initialCapacity;
        private int top;

        public ArrayWrapper(int initialCapacity = 4)
        {
            this.initialCapacity = initialCapacity;
            array = new T[initialCapacity];
            top = 0;
        }

        public T this[int index] 
        { 
            get => array[index]; 
            set => array[index] = value;
        }

        public int Count => array.Length;

        public bool IsReadOnly => array.IsReadOnly;

        public bool IsSynchronized => array.IsSynchronized;

        public object SyncRoot => array.SyncRoot;

        public void Add(T item)
        {
            if (TooSmall())
            {
                Expand();
            }

            array[top] = item;

            top++;
        }

        private bool TooSmall()
        {
            return top == Count;
        }

        private void Expand()
        {
            Array.Resize(ref array, array.Length * 2);
        }

        public bool Contains(T item)
        {
            return ((ICollection<T>)array).Contains(item);
        }

        public bool Remove(T item)
        {
            for (int i = 0; i < top; i++)
            {
                if (array[i].Equals(item))
                {
                    array[i] = default;

                    for (int j = i + 1; j < top; j++)
                    {
                        array[j - 1] = array[j];
                    }

                    top--;

                    if (TooBig())
                    {
                        Contract();
                    }

                    return true;
                }
            }

            return false;
        }

        private bool TooBig()
        {
            return initialCapacity < Count && top < (Count / 4);
        }

        private void Contract()
        {
            Array.Resize(ref array, array.Length / 2);
        }

        public void Clear()
        {
            Array.Clear(array);
            top = 0;
            Array.Resize(ref array, initialCapacity);
        }

        public object Clone()
        {
            return array.Clone();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)this.array).CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < top; i++)
            {
                yield return array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        void ICollection.CopyTo(Array array, int index)
        {
            this.array.CopyTo(array, index);
        }

        object IList.this[int index] { get => ((IList)array)[index]; set => ((IList)array)[index] = value; }

        bool IList.IsFixedSize => false;

        void IList.Insert(int index, object value)
        {
            ((IList)array).Insert(index, value);
        }

        int IList.Add(object value)
        {
            return ((IList)array).Add(value);
        }

        void IList.Remove(object value)
        {
            ((IList)array).Remove(value);
        }

        void IList.RemoveAt(int index)
        {
            ((IList)array).RemoveAt(index);
        }

        bool IList.Contains(object value)
        {
            return ((IList)array).Contains(value);
        }

        int IList.IndexOf(object value)
        {
            return ((IList)array).IndexOf(value);
        }

        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            return ((IStructuralComparable)array).CompareTo(other, comparer);
        }

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
        {
            return ((IStructuralEquatable)array).Equals(other, comparer);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return ((IStructuralEquatable)array).GetHashCode(comparer);
        }
    }
}
