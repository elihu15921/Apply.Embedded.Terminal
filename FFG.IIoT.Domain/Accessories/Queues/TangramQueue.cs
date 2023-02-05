namespace IIoT.Domain.Accessories.Queues;
internal sealed class TangramQueue : ITangramQueue
{
    readonly ITimeserieWrapper _timeserie;
    public TangramQueue(ITimeserieWrapper timeserie) => _timeserie = timeserie;
    public void Push(ITangramConnection.Entity.Meta meta) => Task.Run(async () => await _timeserie.TangramConnection.InsertAsync(new()
    {
        TangramId = meta.TangramId,
        Status = meta.Status,
        Identifier = string.Empty,
        Timestamp = DateTime.UtcNow
    }));
}