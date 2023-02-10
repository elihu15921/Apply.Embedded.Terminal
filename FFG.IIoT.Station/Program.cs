try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.ConfigureHostOptions(item =>
    {
        item.ShutdownTimeout = Menu.Timeout;
    }).AddAppSettingsSecretsJson().UseAutofac().UseSerilog().UseSystemd();
    builder.WebHost.UseKestrel(item => item.ListenAnyIP(Local.Entrance));
    builder.Services.AddRazorPages();
    builder.Services.AddMudServices();
    builder.Services.AddControllers();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddAuthenticationCore();
    builder.Services.AddEndpointsApiExplorer();
    await builder.AddApplicationAsync<AppModule>();
    var apply = builder.Build();
    {
        apply.UseExceptionHandler("/Error");
        apply.UseSerilogRequestLogging();
        apply.UseStaticFiles();
        apply.UseTriggers();
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
    Log.Fatal(Menu.Title, nameof(Program), new
    {
        e.Message,
        e.StackTrace
    });
}
finally
{
    Log.CloseAndFlush();
}