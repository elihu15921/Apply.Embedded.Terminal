namespace IIoT.Domain.Wrappers;

[Volo.Abp.DependencyInjection.Dependency(ServiceLifetime.Singleton)]
file sealed class HostWrapper : IHostWrapper
{
    public IMitsubishiHost Mitsubishi => new MitsubishiHost(Basic, Maintenance);
    public required IBasicFunction Basic { get; init; }
    public required IMaintenanceSource Maintenance { get; init; }
}