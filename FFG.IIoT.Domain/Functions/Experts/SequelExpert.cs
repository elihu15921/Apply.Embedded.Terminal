namespace IIoT.Domain.Functions.Experts;
public abstract class SequelExpert<T> : ISequelExpert<T> where T : ISequelExpert<T>.MetaBase
{
    protected SequelExpert(IBasicExpert basic)
    {
        URL = basic.Profile?.Pool.URL;
        Username = basic.Profile?.Pool.Username;
        Password = basic.Profile?.Pool.Password;
        Organize = basic.Profile?.Pool.Organize;
    }
    public async ValueTask WriteAsync(T meta, string bucket)
    {
        if (URL is not null && Username is not null && Password is not null && Organize is not null)
        {
            using InfluxDBClient result = new(URL, Username, Password);
            await result.GetWriteApiAsync().WriteMeasurementAsync(meta, WritePrecision.Ns, bucket, Organize);
        }
    }
    public async ValueTask WriteAsync(T[] metas, string bucket)
    {
        if (URL is not null && Username is not null && Password is not null && Organize is not null)
        {
            using InfluxDBClient result = new(URL, Username, Password);
            await result.GetWriteApiAsync().WriteMeasurementsAsync(metas, WritePrecision.Ns, bucket, Organize);
        }
    }
    string? URL { get; init; }
    string? Username { get; init; }
    string? Password { get; init; }
    string? Organize { get; init; }
}