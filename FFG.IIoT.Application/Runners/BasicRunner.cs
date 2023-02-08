namespace IIoT.Application.Runners;
internal sealed class BasicRunner : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Basic.Transport.StartAsync();
        while (await new PeriodicTimer(Local.RefreshTime).WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                await Basic.InitialProfileAsync();
                if (Basic.Profile is not null)
                {
                    Local.Language = Basic.Profile.Root.Language;
                    foreach (ITimeserieWrapper.BucketTag item in Enum.GetValues(typeof(ITimeserieWrapper.BucketTag)))
                    {
                        var name = item.GetType().GetDescription(item.ToString());
                        await Basic.InitialPoolAsync(Basic.Profile.Pool.URL, Basic.Profile.Pool.Organize, Basic.Profile.Pool.Username, Basic.Profile.Pool.Password, name);
                    }
                }
                if (Histories.Any()) Histories.Clear();
                System.PushBasicRunner(new DateTime());
            }
            catch (Exception e)
            {
                if (!Histories.Contains(e.Message))
                {
                    Histories.Add(e.Message);
                    Log.Fatal(Menu.Title, nameof(BasicRunner), new { e.Message });
                }
            }
        }
    }
    internal required List<string> Histories { get; init; } = new();
    public required IBasicExpert Basic { get; init; }
    public required ISystemPool System { get; init; }
}