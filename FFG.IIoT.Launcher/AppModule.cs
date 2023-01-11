namespace FFG.IIoT.Launcher;

[DependsOn(typeof(AbpAutofacModule), typeof(AbpAspNetCoreModule), typeof(ApplicationModule))]
internal sealed class AppModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<WeatherForecastService>();
        context.Services.AddCors(item => item.AddDefaultPolicy(item =>
        {
            item.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("*");
        }));
    }
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var service = context.GetApplicationBuilder();
    }
}