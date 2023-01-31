namespace IIoT.Domain.Timeseries.Manages;
internal sealed class MaintenanceYear : SequelExpert<IMaintenanceYear.Entity>, IMaintenanceYear
{
    readonly ILatestPool _latest;
    public MaintenanceYear(IBasicExpert basic, ILatestPool latest) : base(basic) => _latest = latest;
    public async ValueTask InsertAsync(IMaintenanceYear.Entity[] entities)
    {
        var taskAsync = WriteAsync(entities, ITimeserieWrapper.BucketType.Manage.GetDescription());
        {
            _latest.Push(entities);
            await taskAsync;
        }
    }
}