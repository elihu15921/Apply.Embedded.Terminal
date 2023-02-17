namespace IIoT.Domain.Timeseries.Trunks;
internal sealed class FixtureStatus : SequelExpert<IFixtureStatus.Entity>, IFixtureStatus
{
    readonly ILatestPool _latest;
    public FixtureStatus(IBasicExpert basic, ILatestPool latest) : base(basic) => _latest = latest;
    public async ValueTask InsertAsync(IFixtureStatus.Entity entity)
    {
        var taskAsync = WriteAsync(entity, ITimeserieWrapper.BucketTag.Trunk.GetDescription());
        {
            _latest.Push(entity);
            await taskAsync;
        }
    }
}