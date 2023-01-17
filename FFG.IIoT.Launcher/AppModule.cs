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
        context.Services.AddControllers(item =>
        {
            item.ReturnHttpNotAcceptable = true;
            item.Filters.Add<ExceptionFilter>();
        }).ConfigureApiBehaviorOptions(item =>
        {
            item.SuppressModelStateInvalidFilter = default;
            item.InvalidModelStateResponseFactory = context =>
            {
                List<string> results = new();
                results.AddRange(Refresher());
                return new UnprocessableEntityObjectResult(new { Message = string.Join(",\u00A0", results) })
                {
                    ContentTypes = { MediaTypeNames.Application.Json }
                };
                IEnumerable<string> Refresher()
                {
                    foreach (var entry in context.ModelState.Root.Children ?? Enumerable.Empty<ModelStateEntry>())
                    {
                        for (int i = default; i < entry.Errors.Count; i++) yield return entry.Errors[i].ErrorMessage;
                    }
                }
            };
        }).AddNewtonsoftJson(item =>
        {
            item.SerializerSettings.DateFormatString = HistoryFoot.DateFormat;
            item.SerializerSettings.NullValueHandling = NullValueHandling.Include;
        }).AddMvcOptions(item => item.Conventions.Add(new ModelConvention())).AddControllersAsServices();
    }
}