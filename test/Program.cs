using System;
using System.Collections;

namespace test
{
    public class ArrayBase<T> : IArrayOperations<T>, IEnumerable<T>, IEquatable<ArrayBase<T>?>, ICloneable
    {
        protected T[] data;

        public int Length => data.Length;

        public ArrayBase(params int[] dimensions)
        {
            int totalSize = 1;
            foreach (int dimension in dimensions)
            {
                totalSize *= dimension;
            }
            data = new T[totalSize];
        }

        public ArrayBase(T[] dataArray)
        {
            data = dataArray;
        }

        public string ToArrayBaseString()
        {
            return $"ArrayBase of {typeof(T)} with length {Length}";
        }

        public static ArrayBase<T> CreateFromArray(T[] array)
        {
            return new ArrayBase<T>(array);
        }

        public T this[int index]
        {
            get => data[index];
            set => data[index] = value;
        }

        public T Sum()
        {
            dynamic sum = default(T);
            foreach (T item in data)
            {
                sum += item;
            }
            return sum;
        }

        public T Min()
        {
            dynamic min = data[0];
            foreach (T item in data)
            {
                if (Comparer<T>.Default.Compare(item, min) < 0)
                    min = item;
            }
            return min;
        }

        public T Max()
        {
            dynamic max = data[0];
            foreach (T item in data)
            {
                if (Comparer<T>.Default.Compare(item, max) > 0)
                    max = item;
            }
            return max;
        }

        public void Resize(params int[] dimensions)
        {
            int totalSize = 1;
            foreach (int dimension in dimensions)
            {
                totalSize *= dimension;
            }
            Array.Resize(ref data, totalSize);
        }

        public string Description => $"Array of {typeof(T)} with length {Length}";

        public bool Equals(ArrayBase<T>? other)
        {
            if (other == null) return false;
            if (data.Length != other.data.Length) return false;
            for (int i = 0; i < data.Length; i++)
            {
                if (!data[i].Equals(other.data[i]))
                    return false;
            }
            return true;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Equals((ArrayBase<T>?)obj);
        }

        public override int GetHashCode()
        {
            return data.GetHashCode();
        }

        public static bool operator ==(ArrayBase<T>? a, ArrayBase<T>? b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (a is null || b is null)
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(ArrayBase<T>? a, ArrayBase<T>? b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new();
            sb.AppendLine($"Array of {typeof(T)}:");
            foreach (var item in data)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }

        public virtual object Clone()
        {
            ArrayBase<T> clone = new(dataArray: (T[])data.Clone());
            return clone;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            //zaglushka
        }
    }
}
