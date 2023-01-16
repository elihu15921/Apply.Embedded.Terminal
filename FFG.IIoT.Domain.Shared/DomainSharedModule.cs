namespace IIoT.Domain.Shared;

[DependsOn(typeof(AbpLocalizationModule))]
public sealed class DomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().MinimumLevel.Warning()
        .MinimumLevel.Override("System", LogEventLevel.Error)
        .MinimumLevel.Override("Default", LogEventLevel.Error)
        .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
        .MinimumLevel.Override("Volo.Abp.Core", LogEventLevel.Error)
        .MinimumLevel.Override("Volo.Abp.Autofac", LogEventLevel.Error)
        .MinimumLevel.Override("Volo.Abp.AspNetCore", LogEventLevel.Error)
        .WriteTo.File(Path.Combine(HistoryFoot.Location, "Systems", "sys-.log"),
        outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{Exception}{NewLine}",
        rollingInterval: RollingInterval.Day, retainedFileCountLimit: HistoryFoot.RetentionDay).CreateLogger();
    }
}