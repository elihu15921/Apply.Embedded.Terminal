namespace IIoT.Domain;

[DependsOn(typeof(DomainSharedModule))]
public sealed class DomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IBasicExpert, BasicExpert>();
        context.Services.AddSingleton<IInformationController, InformationController>();
        context.Services.AddSingleton<ILifespanController, LifespanController>();
        context.Services.AddSingleton<IMaintenanceController, MaintenanceController>();
    }
}