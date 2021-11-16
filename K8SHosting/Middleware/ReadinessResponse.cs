using System.Text.Json.Serialization;

namespace K8SHosting.Middleware
{
    public class ReadinessResponse : StartupResponse
    {
        [JsonPropertyName("ready")]
        public bool Ready { get; set; }

        public ReadinessResponse(IMicroService service) : base(service)
        {
            Ready = service.IsReady;
        }

        public ReadinessResponse()
        {
        }
    }
}
