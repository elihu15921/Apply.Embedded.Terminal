namespace IIoT.Domain;

[DependsOn(typeof(DomainSharedModule))]
public sealed class DomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IBasicFunction, BasicFunction>();
        context.Services.AddSingleton<IMaintenanceSource, MaintenanceSource>();
    }
}