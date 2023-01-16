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
    await builder.AddApplicationAsync<AppModule>();
    var apply = builder.Build();
    {
        apply.UseExceptionHandler("/Error");
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