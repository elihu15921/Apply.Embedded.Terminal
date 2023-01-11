namespace IIoT.Application.Runners;
internal sealed class BasicRunner : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await new PeriodicTimer(RefreshTime).WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                await Basic.InitialProfile();
            }
            catch (Exception e)
            {
                Log.Fatal("[{0}] {1}", nameof(BasicRunner), new
                {
                    e.Message,
                    e.StackTrace
                });
            }
        }
    }
    public required IBasicFunction Basic { get; init; }
}