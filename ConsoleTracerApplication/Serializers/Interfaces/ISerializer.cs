namespace ConsoleTracerApplication.Serializers.Interfaces
{
    public interface ISerializer<in T>
    {
        void Serialize(T value);
    }
}