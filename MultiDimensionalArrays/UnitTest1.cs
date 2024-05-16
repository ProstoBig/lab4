using NUnit.Framework;
using MultiDimensionalArrays;
using System;

namespace MultiDimensionalArrays.Tests
{
    public class ArrayTests
    {
        private MultiDimensionalArray<int> _array;

        [SetUp]
        public void Setup()
        {
            _array = new MultiDimensionalArray<int>(2, 3, 4);
            Random rand = new Random();
            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = rand.Next(100);
            }
        }

        [Test]
        public void TestSum()
        {
            int expectedSum = 0;
            foreach (var item in _array)
            {
                expectedSum += item;
            }
            int actualSum = _array.Sum();

            Assert.AreEqual(expectedSum, actualSum);
        }

        [Test]
        public void TestMin()
        {
            int expectedMin = _array[0];
            for (int i = 1; i < _array.Length; i++)
            {
                if (_array[i] < expectedMin)
                {
                    expectedMin = _array[i];
                }
            }

            Assert.AreEqual(expectedMin, _array.Min());
        }

        [Test]
        public void TestMax()
        {
            int expectedMax = _array[0];
            for (int i = 1; i < _array.Length; i++)
            {
                if (_array[i] > expectedMax)
                {
                    expectedMax = _array[i];
                }
            }

            Assert.AreEqual(expectedMax, _array.Max());
        }

        [Test]
        public void TestResize()
        {
            int originalLength = _array.Length;

            _array.Resize(3, 4, 5);

            Assert.AreEqual(3 * 4 * 5, _array.Length);
            Assert.AreEqual(originalLength, _array.Length);
        }

        [Test]
        public void TestDescription()
        {
            Assert.AreEqual($"Array of System.Int32 with length {_array.Length}", _array.Description);
        }

    }
}
