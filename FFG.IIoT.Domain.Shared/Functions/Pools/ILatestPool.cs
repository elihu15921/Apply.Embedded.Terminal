namespace IIoT.Domain.Shared.Functions.Pools;
public interface ILatestPool
{
    void Push(IInformationTrunk.Entity entity);
    IInformationTrunk.Entity? Machine { get; }
}