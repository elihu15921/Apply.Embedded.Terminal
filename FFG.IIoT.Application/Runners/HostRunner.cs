namespace IIoT.Application.Runners;
internal sealed class HostRunner : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await new PeriodicTimer(TimeSpan.FromSeconds(10)).WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                if (Basic.Profile is not null)
                {
                    var address = IPAddress.Parse(Basic.Profile.Control.IP);
                    switch (Basic.Profile.Control.Type)
                    {
                        case HostType.Mitsubishi:
                            await Host.Mitsubishi.CreateAsync(address);
                            break;
                    }
                }
                if (Histories.Any()) Histories.Clear();
            }
            catch (Exception e)
            {
                if (!Histories.Contains(e.Message))
                {
                    Histories.Add(e.Message);
                    Log.Fatal(HistoryFoot.Title, nameof(HostRunner), new { e.Message });
                }
            }
        }
    }
    internal required List<string> Histories { get; init; } = new();
    public required IBasicExpert Basic { get; init; }
    public required IHostWrapper Host { get; init; }
}