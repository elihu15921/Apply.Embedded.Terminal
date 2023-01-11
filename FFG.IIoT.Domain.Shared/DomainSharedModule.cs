namespace IIoT.Domain.Shared;

[DependsOn(typeof(AbpLocalizationModule))]
public sealed class DomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext().MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .WriteTo.File(IBasicFunction.LogPath, rollingInterval: RollingInterval.Hour, retainedFileCountLimit: IBasicFunction.RetainedFileCount)
            .CreateBootstrapLogger();
        context.Services.AddSingleton<IMongoClient>(item => new MongoClient(new MongoClientSettings
        {
            Server = new MongoServerAddress(IPAddress.Loopback.ToString(), 27017)
        }));
    }
}