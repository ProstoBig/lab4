namespace test
{
    /// <summary>
    /// Інтерфейс для операцій над масивами.
    /// </summary>
    public interface IArrayOperations<T>
    {
        /// <summary>
        /// Обчислює суму всіх елементів у масиві.
        /// </summary>
        T Sum();

        /// <summary>
        /// Знаходить мінімальне значення у масиві.
        /// </summary>
        T Min();

        /// <summary>
        /// Знаходить максимальне значення у масиві.
        /// </summary>
        T Max();

        /// <summary>
        /// Змінює розмір масиву.
        /// </summary>
        void Resize(params int[] dimensions);

        /// <summary>
        /// Опис масиву.
        /// </summary>
        string Description { get; }
    }
}