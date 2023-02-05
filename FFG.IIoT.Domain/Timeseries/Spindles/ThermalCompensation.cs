namespace IIoT.Domain.Timeseries.Spindles;
internal sealed class ThermalCompensation : SequelExpert<IThermalCompensation.Entity>, IThermalCompensation
{
    readonly ILatestPool _latest;
    public ThermalCompensation(IBasicExpert basic, ILatestPool latest) : base(basic) => _latest = latest;
    public async ValueTask InsertAsync(IThermalCompensation.Entity entity)
    {
        var taskAsync = WriteAsync(entity, ITimeserieWrapper.BucketTag.Spindle.GetDescription());
        {
            _latest.Push(entity);
            await taskAsync;
        }
    }
}