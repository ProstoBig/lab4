using NUnit.Framework;
using System;
using test;

namespace MultiDimensionalArrays.Tests
{
    public class ArrayTests
    {
        private MultiDimensionalArray<int> _array;

        [SetUp]
        public void Setup()
        {
            _array = new MultiDimensionalArray<int>(2, 3, 4);
            Random rand = new();
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
        }

        [Test]
        public void TestDescription()
        {
            Assert.AreEqual($"Array of System.Int32 with length {_array.Length}", _array.Description);
        }

        [Test]
        public void TestClone()
        {
            var clonedArray = (MultiDimensionalArray<int>)_array.Clone();

            Assert.AreEqual(_array.Length, clonedArray.Length);
            for (int i = 0; i < _array.Length; i++)
            {
                Assert.AreEqual(_array[i], clonedArray[i]);
            }
        }

        [Test]
        public void TestEquals()
        {
            var anotherArray = new MultiDimensionalArray<int>(2, 3, 4);
            for (int i = 0; i < anotherArray.Length; i++)
            {
                anotherArray[i] = _array[i];
            }

            Assert.IsTrue(_array.Equals(anotherArray));
            Assert.IsTrue(_array == anotherArray);

            anotherArray[0] = -1;
            Assert.IsFalse(_array.Equals(anotherArray));
            Assert.IsTrue(_array != anotherArray);
        }

        [Test]
        public void TestToArrayBaseString()
        {
            var arrayBase = new ArrayBase<int>(2, 3);
            Assert.AreEqual("ArrayBase of System.Int32 with length 6", arrayBase.ToArrayBaseString());
        }

        [Test]
        public void TestCreateFromArray()
        {
            int[] initialArray = { 1, 2, 3, 4 };
            var createdArray = ArrayBase<int>.CreateFromArray(initialArray);

            Assert.AreEqual(initialArray.Length, createdArray.Length);
            for (int i = 0; i < initialArray.Length; i++)
            {
                Assert.AreEqual(initialArray[i], createdArray[i]);
            }
        }

        [Test]
        public void TestCopyTo()
        {
            var targetArray = new MultiDimensionalArray<int>(2, 3, 4);
            _array.CopyTo(targetArray);

            Assert.AreEqual(_array.Length, targetArray.Length);
            for (int i = 0; i < _array.Length; i++)
            {
                Assert.AreEqual(_array[i], targetArray[i]);
            }
        }

        [Test]
        public void TestIndicesOf()
        {
            _array[0] = 42;
            _array[1] = 42;

            var indices = _array.IndicesOf(42);

            Assert.AreEqual(2, indices.Length);
            Assert.IsTrue(indices.Any(indexArray => indexArray.SequenceEqual(new int[] { 0, 0, 0 })));
            Assert.IsTrue(indices.Any(indexArray => indexArray.SequenceEqual(new int[] { 0, 0, 1 })));
        }

        [Test]
        public void TestIndexer()
        {
            _array[0] = 99;
            Assert.AreEqual(99, _array[0]);
        }

        [Test]
        public void TestSerialization()
        {
            var serializedData = _array.Serialize();
            var deserializedArray = MultiDimensionalArray<int>.Deserialize(serializedData);

            Assert.AreEqual(_array.Length, deserializedArray.Length);
            for (int i = 0; i < _array.Length; i++)
            {
                Assert.AreEqual(_array[i], deserializedArray[i]);
            }
        }
    }
}