namespace IIoT.Domain.Timeseries.Trunks;
internal sealed class BasicInformation : SequelExpert<IBasicInformation.Entity>, IBasicInformation
{
    readonly ILatestPool _latest;
    public BasicInformation(IBasicExpert basic, ILatestPool latest) : base(basic) => _latest = latest;
    public async ValueTask InsertAsync(IBasicInformation.Entity entity)
    {
        var taskAsync = WriteAsync(entity, ITimeserieWrapper.BucketTag.Trunk.GetDescription());
        {
            _latest.Push(entity);
            await taskAsync;
        }
    }
}