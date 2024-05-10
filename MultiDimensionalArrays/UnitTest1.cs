using NUnit.Framework;

namespace MultiDimensionalArrays.Tests
{
    public class ArrayTests
    {
        private MultiDimensionalArray<int> _array;

        [SetUp]
        public void Setup()
        {
            _array = new MultiDimensionalArray<int>(2, 3, 4);
            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = i * 3;
            }
        }

        [Test]
        public void TestSum()
        {
            // Arrange
            MultiDimensionalArray<int> array = new MultiDimensionalArray<int>(2, 3, 4);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }

            // Act
            int expectedSum = (array.Length - 1) * array.Length / 2; // Sum of integers from 0 to n-1
            int actualSum = array.Sum();

            // Assert
            Assert.AreEqual(expectedSum, actualSum);
        }

        [Test]
        public void TestMin()
        {
            Assert.AreEqual(0, _array.Min());
        }

        [Test]
        public void TestMax()
        {
            // Arrange
            MultiDimensionalArray<int> array = new MultiDimensionalArray<int>(2, 3, 4);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }

            // Act
            int expectedMax = array.Length - 1; // Maximum value is the last index
            int actualMax = array.Max();

            // Assert
            Assert.AreEqual(expectedMax, actualMax);
        }

        [Test]
        public void TestResize()
        {
            _array.Resize(3, 4, 5);
            Assert.AreEqual(60, _array.Length);
        }

        [Test]
        public void TestDescription()
        {
            Assert.AreEqual("Array of System.Int32 with length 24", _array.Description);
        }

        [Test]
        public void TestMethod1()
        {
            // Arrange
            ArrayBase<int> array = new ArrayBase<int>(new int[] { 1, 2, 3, 4, 5 });

            // Act
            array.Method1();

            // Assert
            // Here you can add assertions to verify the behavior of Method1
        }

        [Test]
        public void TestProperty()
        {
            // Arrange
            ArrayBase<int> array = new ArrayBase<int>(new int[] { 1, 2, 3, 4, 5 });

            // Act
            array.Property = "Test";

            // Assert
            Assert.AreEqual("Test", array.Property);
        }
    }
}
