namespace IIoT.Application.Runners;
internal sealed class HostRunner : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await new PeriodicTimer(Menu.RefreshTime).WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                if (Basic.Profile is not null)
                {
                    var address = IPAddress.Parse(Basic.Profile.Control.IP);
                    switch (Basic.Profile.Control.Type)
                    {
                        case HostType.Mitsubishi:
                            {
                                CancellationTokenSource watchdogSource = new();
                                CancellationTokenSource hostSource = new();
                                _ = Task.Run(async () =>
                                {
                                    await Task.Delay(Menu.Timeout);
                                    if (!watchdogSource.IsCancellationRequested)
                                    {
                                        hostSource.Cancel();
                                        Host.Mitsubishi.Dispose();
                                    }
                                }, watchdogSource.Token);
                                await Host.Mitsubishi.CreateAsync(address, hostSource.Token);
                                watchdogSource.Cancel();
                            }
                            break;
                    }
                }
                if (Histories.Any()) Histories.Clear();
                System.PushHostRunner(DateTime.UtcNow);
            }
            catch (Exception e)
            {
                if (!Histories.Contains(e.Message))
                {
                    Histories.Add(e.Message);
                    Log.Fatal(Menu.Title, nameof(HostRunner), new { e.Message });
                }
            }
        }
    }
    internal required List<string> Histories { get; init; } = new();
    public required IBasicExpert Basic { get; init; }
    public required ISystemPool System { get; init; }
    public required IHostWrapper Host { get; init; }
}