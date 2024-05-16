namespace test
{
    public interface IArrayOperations<T>
    {
        T Sum();
        T Min();
        T Max();
        void Resize(params int[] dimensions);
        string Description { get; }
    }
}