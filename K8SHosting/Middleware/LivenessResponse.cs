using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace K8SHosting.Middleware
{
    public class LivenessResponse : MiddlewareResponse
    {
        public LivenessResponse(IMicroService service)
        : base(service)
        {
            var asm = Assembly.GetEntryAssembly();
            var versions = new Dictionary<string, string>();

            versions[asm.GetName().Name] = asm.GetName().Version.ToString();

            foreach (var assembly in asm.GetReferencedAssemblies())
            {
                if (assembly.Name != null && assembly.Version != null)
                {
                    versions[assembly.Name] = assembly.Version.ToString();
                }
            }

            Versions = versions.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        [JsonPropertyName("versions")]
        public Dictionary<string, string> Versions { get; set; } = new Dictionary<string, string>();
    }
}
