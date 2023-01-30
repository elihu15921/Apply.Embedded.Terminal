namespace IIoT.Domain.Functions.Pools;
internal sealed class LatestPool : ILatestPool
{
    public void Push(IInformationTrunk.Entity entity) => Machine = entity;
    public IInformationTrunk.Entity? Machine { get; private set; }
}