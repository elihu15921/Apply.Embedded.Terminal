namespace IIoT.Domain;

[DependsOn(typeof(DomainSharedModule))]
public sealed class DomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<ILatestPool, LatestPool>();
        context.Services.AddSingleton<ISystemPool, SystemPool>();
        context.Services.AddSingleton<IBasicExpert, BasicExpert>();
    }
}