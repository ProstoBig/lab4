using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace test
{
    /// <summary>
    /// Представляє багатовимірний масив.
    /// </summary>
    [Serializable]
    public class MultiDimensionalArray<T> : ArrayBase<T>
    {
        private readonly int[] dimensions;
        /// <summary>
        /// Ініціалізує новий екземпляр класу з вказаними розмірами.
        /// </summary>
        public MultiDimensionalArray(params int[] dimensions) : base(dimensions)
        {
            this.dimensions = dimensions;
        }
        /// <summary>
        /// Отримує форму масиву.
        /// </summary>
        public int[] Shape => dimensions;

        /// <summary>
        /// Копіює вміст цього масиву в інший.
        /// </summary>
        public void CopyTo(MultiDimensionalArray<T> array)
        {
            if (!Shape.SequenceEqual(array.Shape))
            {
                throw new ArgumentException("Shapes of the arrays don't match");
            }
            Array.Copy(data, array.data, data.Length);
        }
        /// <summary>
        /// Отримує індекси значень у масиві.
        /// </summary>
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
        /// <summary>
        /// Повертає клон цього об'єкту.
        /// </summary>
        public override object Clone()
        {
            MultiDimensionalArray<T> clone = new(dimensions);
            Array.Copy(data, clone.data, data.Length);
            return clone;
        }
        /// <summary>
        /// Серіалізує об'єкт в масив байтів.
        /// </summary>
        public byte[] Serialize()
        {
            using MemoryStream ms = new();
            BinaryFormatter formatter = new();
            formatter.Serialize(ms, this);
            return ms.ToArray();
        }
        /// <summary>
        /// Десеріалізує масив байтів в об'єкт.
        /// </summary>
        public static new MultiDimensionalArray<T> Deserialize(byte[] data)
        {
            using MemoryStream ms = new(data);
            BinaryFormatter formatter = new();
            return (MultiDimensionalArray<T>)formatter.Deserialize(ms);
        }
    }
}

