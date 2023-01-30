namespace IIoT.Domain.Functions.Experts;
public abstract class SequelExpert<T> : ISequelExpert<T> where T : ISequelExpert<T>.MetaBase
{
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
    public string? URL { get; init; }
    public string? Username { get; init; }
    public string? Password { get; init; }
    public string? Organize { get; init; }
}