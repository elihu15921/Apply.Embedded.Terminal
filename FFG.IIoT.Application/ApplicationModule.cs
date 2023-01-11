namespace IIoT.Application;

[DependsOn(typeof(ApplicationContractModule))]
public sealed class ApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHostedService<BasicRunner>();
        context.Services.AddHostedService<HostRunner>();
    }
}