namespace IIoT.Domain.Accessories.Queues;
internal sealed class IcpdasQueue : IIcpdasQueue
{
    readonly ITimeserieWrapper _timeserie;
    public IcpdasQueue(ITimeserieWrapper timeserie) => _timeserie = timeserie;
}