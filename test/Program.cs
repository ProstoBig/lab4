using System;
using System.Collections;
using System.Collections.Generic;

public interface IArrayOperations<T>
{
    T Sum();
    T Min();
    T Max();
    void Resize(params int[] dimensions);
    string Description { get; }
}

public class ArrayBase<T> : IArrayOperations<T>, IEnumerable<T>, IEquatable<ArrayBase<T>>, ICloneable
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

    public bool Equals(ArrayBase<T> other)
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

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        return Equals((ArrayBase<T>)obj);
    }

    public override int GetHashCode()
    {
        return data.GetHashCode();
    }

    public static bool operator ==(ArrayBase<T> a, ArrayBase<T> b)
    {
        if (ReferenceEquals(a, b))
            return true;
        if (a is null || b is null)
            return false;
        return a.Equals(b);
    }

    public static bool operator !=(ArrayBase<T> a, ArrayBase<T> b)
    {
        return !(a == b);
    }

    public override string ToString()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine($"Array of {typeof(T)}:");
        foreach (var item in data)
        {
            sb.AppendLine(item.ToString());
        }
        return sb.ToString();
    }

    public object Clone()
    {
        ArrayBase<T> clone = new ArrayBase<T>();
        Array.Copy(data, clone.data, data.Length);
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

public class MultiDimensionalArray<T> : ArrayBase<T>
{
    private readonly int[] dimensions;

    public MultiDimensionalArray(params int[] dimensions) : base(dimensions)
    {
        this.dimensions = dimensions;
    }

    public int[] Shape => dimensions;

    public void CopyTo(MultiDimensionalArray<T> array)
    {
        if (!Shape.SequenceEqual(array.Shape))
        {
            throw new ArgumentException("Shapes of the arrays don't match");
        }
        Array.Copy(data, array.data, data.Length);
    }

    public int[][] IndicesOf(T value)
    {
        List<int[]> indicesList = new List<int[]>();
        for (int i = 0; i < data.Length; i++)
        {
            if (EqualityComparer<T>.Default.Equals(data[i], value))
            {
                indicesList.Add(ConvertIndexToIndices(i));
            }
        }
        return indicesList.ToArray();
    }

    private int[] ConvertIndexToIndices(int index)
    {
        int[] indices = new int[dimensions.Length];
        int multiplier = 1;
        for (int i = dimensions.Length - 1; i >= 0; i--)
        {
            indices[i] = (index / multiplier) % dimensions[i];
            multiplier *= dimensions[i];
        }
        return indices;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Тестування
        MultiDimensionalArray<int> multiArr = new MultiDimensionalArray<int>(2, 3, 4);
        for (int i = 0; i < multiArr.Length; i++)
        {
            multiArr[i] = i * 3;
        }
        Console.WriteLine(multiArr.Description);
        Console.WriteLine("Array: ");
        Console.WriteLine(multiArr);

        MultiDimensionalArray<int> copiedArray = new MultiDimensionalArray<int>(2, 3, 4);
        multiArr.CopyTo(copiedArray);
        Console.WriteLine("Copied Array: ");
        Console.WriteLine(copiedArray);

        int[][] indices = copiedArray.IndicesOf(6);
        Console.WriteLine("Indices of value 6:");
        foreach (var index in indices)
        {
            Console.WriteLine($"Index: {string.Join(", ", index)}");
        }
    }
}
