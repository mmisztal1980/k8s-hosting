using System.Text.Json.Serialization;

namespace K8SHosting.Middleware
{
    public class MiddlewareResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("mode")]
        public MicroServiceMode Mode { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        public MiddlewareResponse(IMicroService service)
        {
            Name = service.Name;
            Id = service.Id;
            Mode = service.Mode;
            //Address = service.Address.ToString();
        }

        public MiddlewareResponse()
        {
        }
    }
}
