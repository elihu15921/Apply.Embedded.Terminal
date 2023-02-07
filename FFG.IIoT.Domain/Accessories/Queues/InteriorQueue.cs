namespace IIoT.Domain.Accessories.Queues;
internal sealed class InteriorQueue : IInteriorQueue
{
    readonly ITimeserieWrapper _timeserie;
    public InteriorQueue(ITimeserieWrapper timeserie) => _timeserie = timeserie;
    public void Push(string deviceName, string rawData) => Task.Run(async () =>
    {
        var datas = rawData.Split('*');
        await _timeserie.ThermalCompensation.InsertAsync(new()
        {
            DeviceName = deviceName,
            ThermalFirst = float.Parse(datas[1]),
            ThermalSecond = float.Parse(datas[3]),
            ThermalThird = float.Parse(datas[5]),
            ThermalFourth = float.Parse(datas[7]),
            ThermalFifth = float.Parse(datas[9]),
            CompensationX = float.Parse(datas[11]),
            CompensationY = float.Parse(datas[13]),
            CompensationZ = float.Parse(datas[15]),
            Identifier = string.Empty,
            Timestamp = DateTime.UtcNow
        });
    });
}