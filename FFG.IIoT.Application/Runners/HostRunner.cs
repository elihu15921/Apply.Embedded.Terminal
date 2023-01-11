namespace IIoT.Application.Runners;
internal sealed class HostRunner : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await new PeriodicTimer(RefreshTime).WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                if (Basic.Profile is not null)
                {
                    var address = IPAddress.Parse(Basic.Profile.Control.Address);

                    Mitsubishi.Warship = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    await Mitsubishi.Warship.ConnectAsync(new IPEndPoint(address, IMitsubishiSummit.Port));
                    await Mitsubishi.InfrastructureAsync(150, 4, IMitsubishiSummit.DeviceCode.D);
                    {
                        Mitsubishi.Warship.Shutdown(SocketShutdown.Both);
                        Mitsubishi.Warship.Close();
                        Mitsubishi.Warship.Dispose();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Fatal("[{0}] {1}", nameof(HostRunner), new
                {
                    e.Message,
                    e.StackTrace
                });
            }
        }
    }
    public required IBasicFunction Basic { get; init; }
    public required IMitsubishiSummit Mitsubishi { get; init; }
}