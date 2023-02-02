namespace IIoT.Domain.Timeseries.Manages;
internal sealed class MaintenanceWeekly : SequelExpert<IMaintenanceWeekly.Entity>, IMaintenanceWeekly
{
    readonly ILatestPool _latest;
    public MaintenanceWeekly(IBasicExpert basic, ILatestPool latest) : base(basic) => _latest = latest;
    public async ValueTask InsertAsync(IMaintenanceWeekly.Entity[] entities)
    {
        var taskAsync = WriteAsync(entities, ITimeserieWrapper.BucketTag.Manage.GetDescription());
        {
            _latest.Push(entities);
            await taskAsync;
        }
    }
}