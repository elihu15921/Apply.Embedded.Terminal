namespace IIoT.Domain.Wrappers;

[Volo.Abp.DependencyInjection.Dependency(ServiceLifetime.Singleton)]
file sealed class TurboWrapper : ITurboWrapper
{
    public IMitsubishiHost Mitsubishi => new MitsubishiHost(Meta, Basic, Lifespan, Maintenance);
    public required IMetaWrapper Meta { get; init; }
    public required IBasicExpert Basic { get; init; }
    public required ILifespanTurbo Lifespan { get; init; }
    public required IMaintenanceTurbo Maintenance { get; init; }
}