namespace IIoT.Domain.Wrappers;

[Volo.Abp.DependencyInjection.Dependency(ServiceLifetime.Singleton)]
file sealed class HostWrapper : IHostWrapper
{
    public IMitsubishiHost Mitsubishi => new MitsubishiHost(Basic, Timeserie);
    public required IBasicExpert Basic { get; init; }
    public required ITimeserieWrapper Timeserie { get; init; }
}