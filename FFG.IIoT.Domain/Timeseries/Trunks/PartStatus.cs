namespace IIoT.Domain.Timeseries.Trunks;
internal sealed class PartStatus : SequelExpert<IPartStatus.Entity>, IPartStatus
{
    readonly ILatestPool _latest;
    public PartStatus(IBasicExpert basic, ILatestPool latest) : base(basic) => _latest = latest;
    public async ValueTask InsertAsync(IPartStatus.Entity entity)
    {
        var taskAsync = WriteAsync(entity, ITimeserieWrapper.BucketTag.Trunk.GetDescription());
        {
            _latest.Push(entity);
            await taskAsync;
        }
    }
}