namespace IIoT.Domain.Timeseries.Manages;
internal sealed class MaintenanceMonthly : SequelExpert<IMaintenanceMonthly.Entity>, IMaintenanceMonthly
{
    readonly ILatestPool _latest;
    public MaintenanceMonthly(IBasicExpert basic, ILatestPool latest) : base(basic) => _latest = latest;
    public async ValueTask InsertAsync(IMaintenanceMonthly.Entity[] entities)
    {
        var taskAsync = WriteAsync(entities, ITimeserieWrapper.BucketType.Manage.GetDescription());
        {
            _latest.Push(entities);
            await taskAsync;
        }
    }
}