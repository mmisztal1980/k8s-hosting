namespace K8SHosting.Shutdown
{
    public interface IActiveRequestsService
    {
        void Increment();
        void Decrement();

        long Counter { get; }

        bool HasActiveRequests { get; }

    }
}
