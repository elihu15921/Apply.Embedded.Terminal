﻿namespace IIoT.Domain.Shared;

[DependsOn(typeof(AbpLocalizationModule), typeof(AbpVirtualFileSystemModule))]
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
        Configure<AbpVirtualFileSystemOptions>(item => item.FileSets.AddEmbedded<DomainSharedModule>(Assembly.GetExecutingAssembly().GetRootNamespace()));
        Configure<AbpLocalizationOptions>(item =>
        {
            item.Resources.Add<Fielder>(Language).AddVirtualJson($"/{string.Join("/", new string[]
            {
                nameof(Languages), nameof(Languages.Fielders)
            })}");
        });
    }
}