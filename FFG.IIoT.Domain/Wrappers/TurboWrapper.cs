namespace IIoT.Domain.Wrappers;

[Volo.Abp.DependencyInjection.Dependency(ServiceLifetime.Singleton)]
file sealed class TurboWrapper : ITurboWrapper
{
    public IMitsubishiHost Mitsubishi => new MitsubishiHost(Basic, Timeserie, Maintenance);
    public required IBasicExpert Basic { get; init; }
    public required ITimeserieWrapper Timeserie { get; init; }
    public required IMaintenanceTurbo Maintenance { get; init; }
}