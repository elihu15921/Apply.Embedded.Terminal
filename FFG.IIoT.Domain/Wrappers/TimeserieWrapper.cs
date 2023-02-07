namespace IIoT.Domain.Wrappers;

[Volo.Abp.DependencyInjection.Dependency(ServiceLifetime.Singleton)]
file sealed class TimeserieWrapper : ITimeserieWrapper
{
    #region Manages  
    public IMaintenanceWeekly MaintenanceWeekly => new MaintenanceWeekly(Basic, Latest);
    public IMaintenanceMonthly MaintenanceMonthly => new MaintenanceMonthly(Basic, Latest);
    public IMaintenanceYear MaintenanceYear => new MaintenanceYear(Basic, Latest);
    #endregion

    #region Spindles
    public ILifespanSpeed LifespanSpeed => new LifespanSpeed(Basic, Latest);
    public IThermalCompensation ThermalCompensation => new ThermalCompensation(Basic, Latest);
    #endregion

    #region Trunks
    public IBasiceInformation BasiceInformation => new BasiceInformation(Basic, Latest);
    public IPartStatus PartStatus => new PartStatus(Basic, Latest);
    #endregion

    #region Universals
    public ITangramConnection TangramConnection => new TangramConnection(Basic, Latest);
    #endregion

    public required IBasicExpert Basic { get; init; }
    public required ILatestPool Latest { get; init; }
}