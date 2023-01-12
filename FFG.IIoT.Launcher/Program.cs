try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.ConfigureHostOptions(item =>
    {
        item.ShutdownTimeout = TimeSpan.FromMinutes(10);
    }).AddAppSettingsSecretsJson().UseAutofac().UseSerilog().UseSystemd();
    builder.WebHost.UseKestrel(item => item.ListenAnyIP(17770));
    builder.Services.AddRazorPages();
    builder.Services.AddControllers();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Open API - v1", Version = "v1" });
    });
    await builder.AddApplicationAsync<AppModule>();
    var apply = builder.Build();
    {
        if (apply.Environment.IsDevelopment())
        {
            apply.UseSwagger();
            apply.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
            apply.UseExceptionHandler("/Error");
        }
        apply.UseSerilogRequestLogging();
        apply.UseStaticFiles();
        apply.UseCors();
        apply.UseRouting();
        apply.UseAuthentication();
        apply.UseAuthorization();
        apply.MapControllers();
        apply.MapBlazorHub();
        apply.MapFallbackToPage("/_Host");
        await apply.InitializeApplicationAsync();
        await apply.RunAsync();
    }
}
catch (Exception e)
{
    Log.Fatal(HistoryFoot.Title, nameof(Program), new
    {
        e.Message,
        e.StackTrace
    });
}
finally
{
    Log.CloseAndFlush();
}