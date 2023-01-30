using static IIoT.Domain.Shared.Divisions.Metas.Trunks.IInformationTrunk;

namespace IIoT.Domain.Divisions.Metas.Trunks;
internal sealed class InformationTrunk : SequelExpert<Entity>, IInformationTrunk
{
    readonly ILatestPool _latest;
    public InformationTrunk(IBasicExpert basic, ILatestPool latest)
    {
        _latest = latest;
        URL = basic.Profile?.Pool.URL;
        Username = basic.Profile?.Pool.Username;
        Password = basic.Profile?.Pool.Password;
        Organize = basic.Profile?.Pool.Organize;
    }
    public async ValueTask BuildAsync()
    {
        using var result = new InfluxDBClient(URL, Username, Password);
        var bucket = await result.GetBucketsApi().FindBucketByNameAsync(IMetaWrapper.BucketTag.Trunk);
        if (bucket is null)
        {
            var organizations = await result.GetOrganizationsApi().FindOrganizationsAsync(org: Organize);
            BucketRetentionRules rule = new(BucketRetentionRules.TypeEnum.Expire, 100 * ISequelExpert<Entity>.Day);
            await result.GetBucketsApi().CreateBucketAsync(IMetaWrapper.BucketTag.Trunk, rule, organizations[default].Id);
        }
    }
    public async ValueTask InsertAsync(Entity entity)
    {
        var taskAsync = WriteAsync(entity, IMetaWrapper.BucketTag.Trunk);
        {
            _latest.Push(entity);
            await taskAsync;
        }
    }
}