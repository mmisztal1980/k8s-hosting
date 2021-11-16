using System.Threading;
using System.Threading.Tasks;

namespace K8SHosting.Startup
{
    public interface IHostedStartupService
    {
        Task StartAsync(CancellationToken cancellationToken);

        bool Completed { get; }
    }
}