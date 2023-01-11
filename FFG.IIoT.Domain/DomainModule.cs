namespace IIoT.Domain;

[DependsOn(typeof(DomainSharedModule))]
public class DomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IBasicFunction, BasicFunction>();
        context.Services.AddSingleton<IMitsubishiSummit, MitsubishiSummit>();
    }
}