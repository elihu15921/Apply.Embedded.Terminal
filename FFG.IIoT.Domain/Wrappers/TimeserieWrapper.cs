namespace IIoT.Domain.Wrappers;

[Volo.Abp.DependencyInjection.Dependency(ServiceLifetime.Singleton)]
file sealed class TimeserieWrapper : ITimeserieWrapper
{
    public IBasicInformation BasicInformation => new BasicInformation(Basic, Latest);
    public IMaintenanceWeekly MaintenanceWeekly => new MaintenanceWeekly(Basic, Latest);
    public IMaintenanceMonthly MaintenanceMonthly => new MaintenanceMonthly(Basic, Latest);
    public IMaintenanceYear MaintenanceYear => new MaintenanceYear(Basic, Latest);
    public ILifespanSpeed LifespanSpeed => new LifespanSpeed(Basic, Latest);
    public required IBasicExpert Basic { get; init; }
    public required ILatestPool Latest { get; init; }
}