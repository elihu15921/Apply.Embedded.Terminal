namespace IIoT.Domain.Functions.Pools;
internal sealed class SystemPool : ISystemPool
{
    public void PushBasicRunner(in DateTime time) => BasicRunnerTimestamp = time;
    public void PushHostRunner(in DateTime time) => HostRunnerTimestamp = time;
    public DateTime BasicRunnerTimestamp { get; private set; }
    public DateTime HostRunnerTimestamp { get; private set; }
}