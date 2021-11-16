using System.Threading;

namespace K8SHosting
{
    public interface IMicroServiceLifetime
    {
        CancellationToken ServiceStarted { get; }
        CancellationToken StartupFailed { get; }
    }
}