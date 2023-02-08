namespace IIoT.Domain.Shared.Functions.Pools;
public interface ISystemPool
{
    void PushBasicRunner(in DateTime time);
    void PushHostRunner(in DateTime time);
    DateTime BasicRunnerTimestamp { get; }
    DateTime HostRunnerTimestamp { get; }
}