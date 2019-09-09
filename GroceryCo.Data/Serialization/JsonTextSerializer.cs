using Newtonsoft.Json;

namespace GroceryCo.Data.Serialization
{
    public class JsonTextSerializer : ITextSerializer
    {
        public T Deserialize<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text);
        }
    }
}
