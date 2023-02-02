namespace IIoT.Domain.Wrappers;

[Volo.Abp.DependencyInjection.Dependency(ServiceLifetime.Singleton)]
file sealed class QueueWrapper : IQueueWrapper
{
    public IIcpdasQueue Icpdas => new IcpdasQueue();
    public ITangramQueue Tangram => new TangramQueue(Timeserie);
    public required ITimeserieWrapper Timeserie { get; init; }
}