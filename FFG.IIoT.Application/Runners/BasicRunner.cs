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
                if (Basic.Profile is not null)
                {
                    Language = Basic.Profile.Root.Language;
                }
                if (Histories.Any()) Histories.Clear();
            }
            catch (Exception e)
            {
                if (!Histories.Contains(e.Message))
                {
                    Histories.Add(e.Message);
                    Log.Fatal(HistoryFoot.Title, nameof(BasicRunner), new { e.Message });
                }
            }
        }
    }
    internal required List<string> Histories { get; init; } = new();
    public required IBasicExpert Basic { get; init; }
}