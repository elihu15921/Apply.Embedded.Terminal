namespace IIoT.Domain.Timeseries.Trunks;
internal sealed class BasiceInformation : SequelExpert<IBasiceInformation.Entity>, IBasiceInformation
{
    readonly ILatestPool _latest;
    public BasiceInformation(IBasicExpert basic, ILatestPool latest) : base(basic) => _latest = latest;
    public async ValueTask InsertAsync(IBasiceInformation.Entity entity)
    {
        var taskAsync = WriteAsync(entity, ITimeserieWrapper.BucketTag.Trunk.GetDescription());
        {
            _latest.Push(entity);
            await taskAsync;
        }
    }
}