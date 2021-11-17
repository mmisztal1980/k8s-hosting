namespace K8SHosting.Shutdown
{
    public interface IActiveRequestsService
    {
        void Increment();
        void Decrement();

        bool HasActiveRequests { get; }

    }
}
