using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;

namespace K8SHosting
{
    public enum MicroServiceMode
    {
        NotSet = 0,
        WebApi,
        GraphQL
    }

    public interface IMicroService
    {
        string Name { get; }
        string Id { get; }
        MicroServiceMode Mode { get; }
        WebApplication App { get; }
        bool IsReady { get; set; }
        bool IsStarted { get; }

        Task RunAsync(string[] args);
    }
}
