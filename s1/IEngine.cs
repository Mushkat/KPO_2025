namespace s1
{
    public interface IEngine
    {
        string EngineType { get; }
        bool IsCompatible(Customer customer);
    }
}