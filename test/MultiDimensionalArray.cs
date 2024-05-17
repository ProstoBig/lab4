using System;
using System.Collections.Generic;
using System.Linq;

namespace test
{
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
            List<int[]> indicesList = new();
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

        public override object Clone()
        {
            MultiDimensionalArray<T> clone = new(dimensions);
            Array.Copy(data, clone.data, data.Length);
            return clone;
        }
    }
}
