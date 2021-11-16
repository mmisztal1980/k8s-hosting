using System.Threading;

namespace K8SHosting
{
    public class MicroServiceLifetime : IMicroServiceLifetime
    {
        internal CancellationTokenSource StartupFailedTokenSource { get; } = new CancellationTokenSource();

        public CancellationToken StartupFailed => StartupFailedTokenSource.Token;

        public CancellationTokenSource ServiceStartedTokenSource { get; } = new CancellationTokenSource();

        public CancellationToken ServiceStarted => ServiceStartedTokenSource.Token;
    }
}
