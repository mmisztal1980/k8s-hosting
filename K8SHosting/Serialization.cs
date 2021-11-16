using System.Text.Json;
using System.Text.Json.Serialization;

namespace K8SHosting
{
    public static class Serialization
    {
        public static class JsonOptions
        {
            static JsonOptions()
            {
                DefaultIndented = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    WriteIndented = true
                };
                DefaultIndented.Converters.Add(new JsonStringEnumConverter());
            }

            public static JsonSerializerOptions DefaultIndented;
        }
    }
}
