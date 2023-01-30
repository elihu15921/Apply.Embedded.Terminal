namespace IIoT.Domain.Wrappers;

[Volo.Abp.DependencyInjection.Dependency(ServiceLifetime.Singleton)]
file sealed class MetaWrapper : IMetaWrapper
{
    public IInformationTrunk Information => new InformationTrunk(Basic, Latest);
    public required IBasicExpert Basic { get; init; }
    public required ILatestPool Latest { get; init; }
}