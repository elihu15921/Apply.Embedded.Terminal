namespace IIoT.Domain.Timeseries.Trunks;
internal sealed class AlarmStatus : SequelExpert<IAlarmStatus.Entity>, IAlarmStatus
{
    readonly ILatestPool _latest;
    public AlarmStatus(IBasicExpert basic, ILatestPool latest) : base(basic) => _latest = latest;
    public async ValueTask InsertAsync(IAlarmStatus.Entity entity)
    {
        var taskAsync = WriteAsync(entity, ITimeserieWrapper.BucketTag.Trunk.GetDescription());
        {
            _latest.Push(entity);
            await taskAsync;
        }
    }
}