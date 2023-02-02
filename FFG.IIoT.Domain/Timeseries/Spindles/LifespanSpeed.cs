namespace IIoT.Domain.Timeseries.Spindles;
internal sealed class LifespanSpeed : SequelExpert<ILifespanSpeed.Entity>, ILifespanSpeed
{
    readonly ILatestPool _latest;
    public LifespanSpeed(IBasicExpert basic, ILatestPool latest) : base(basic) => _latest = latest;
    public async ValueTask InsertAsync(ILifespanSpeed.Entity[] entities)
    {
        var taskAsync = WriteAsync(entities, ITimeserieWrapper.BucketTag.Spindle.GetDescription());
        {
            _latest.Push(entities);
            await taskAsync;
        }
    }
}