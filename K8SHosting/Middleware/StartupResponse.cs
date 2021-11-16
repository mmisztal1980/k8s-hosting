using System.Text.Json.Serialization;

namespace K8SHosting.Middleware
{
    public class StartupResponse : MiddlewareResponse
    {
        [JsonPropertyName("started")]
        public bool Started { get; set; }

        public StartupResponse(IMicroService service)
        : base(service)
        {
            Started = service.IsStarted;
        }

        public StartupResponse()
        {
        }
    }
}
