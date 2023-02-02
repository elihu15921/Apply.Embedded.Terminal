namespace IIoT.Domain.Accessories.Queues;
internal sealed class TangramQueue : ITangramQueue
{
    readonly ITimeserieWrapper _timeserie;
    public TangramQueue(ITimeserieWrapper timeserie) => _timeserie = timeserie;
    public async ValueTask PushAsync(ITangramConnection.Entity.Meta meta) => await _timeserie.TangramConnection.InsertAsync(new()
    {
        TangramId = meta.TangramId,
        Status = meta.Status,
        Identifier = string.Empty,
        Timestamp = DateTime.UtcNow
    });
}