namespace GroceryCo.Data.Serialization
{
    public interface ITextSerializer
    {
        T Deserialize<T>(string text);
    }
}
