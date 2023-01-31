namespace IIoT.Application.Runners;
internal sealed class BasicRunner : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await new PeriodicTimer(RefreshTime).WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                await Basic.InitialProfileAsync();
                if (Basic.Profile is not null)
                {
                    Language = Basic.Profile.Root.Language;
                    foreach (ITimeserieWrapper.BucketType item in Enum.GetValues(typeof(ITimeserieWrapper.BucketType)))
                    {
                        var name = item.GetType().GetDescription(item.ToString());
                        await Basic.InitialPoolAsync(Basic.Profile.Pool.URL, Basic.Profile.Pool.Organize, Basic.Profile.Pool.Username, Basic.Profile.Pool.Password, name);
                    }
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