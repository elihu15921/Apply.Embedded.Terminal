namespace IIoT.Domain.Timeseries.Universals;
internal sealed class TangramConnection : SequelExpert<ITangramConnection.Entity>, ITangramConnection
{
    readonly ILatestPool _latest;
    public TangramConnection(IBasicExpert basic, ILatestPool latest) : base(basic) => _latest = latest;
    public async ValueTask InsertAsync(ITangramConnection.Entity entity)
    {
        var taskAsync = WriteAsync(entity, ITimeserieWrapper.BucketTag.Tangram.GetDescription());
        {
            _latest.Push(entity);
            await taskAsync;
        }
    }
}