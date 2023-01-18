namespace IIoT.Domain.Wrappers;

[Volo.Abp.DependencyInjection.Dependency(ServiceLifetime.Singleton)]
file sealed class HostWrapper : IHostWrapper
{
    public IMitsubishiHost Mitsubishi => new MitsubishiHost(Basic, Lifespan, Information, Maintenance);
    public required IBasicExpert Basic { get; init; }
    public required ILifespanController Lifespan { get; init; }
    public required IInformationController Information { get; init; }
    public required IMaintenanceController Maintenance { get; init; }
}