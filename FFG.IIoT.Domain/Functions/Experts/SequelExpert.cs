namespace IIoT.Domain.Functions.Experts;
public abstract class SequelExpert<T> : ISequelExpert<T> where T : ISequelExpert<T>.MetaBase
{
    protected SequelExpert(IBasicExpert basic)
    {
        URL = basic.Profile?.Pool.URL;
        UserName = basic.Profile?.Pool.UserName;
        Password = basic.Profile?.Pool.Password;
        Organize = basic.Profile?.Pool.Organize;
    }
    public async ValueTask WriteAsync(T meta, string bucket)
    {
        if (URL is not null && UserName is not null && Password is not null && Organize is not null)
        {
            using InfluxDBClient result = new(URL, UserName, Password);
            await result.GetWriteApiAsync().WriteMeasurementAsync(meta, WritePrecision.Ns, bucket, Organize);
        }
    }
    public async ValueTask WriteAsync(T[] metas, string bucket)
    {
        if (URL is not null && UserName is not null && Password is not null && Organize is not null)
        {
            using InfluxDBClient result = new(URL, UserName, Password);
            await result.GetWriteApiAsync().WriteMeasurementsAsync(metas, WritePrecision.Ns, bucket, Organize);
        }
    }
    public T[] Read(in string bucket, string identifier, DateTimeOffset startTime, DateTimeOffset endTime)
    {
        if (URL is not null && UserName is not null && Password is not null && Organize is not null)
        {
            using InfluxDBClient result = new(URL, UserName, Password);
            return InfluxDBQueryable<T>.Queryable(bucket, Organize, result.GetQueryApiSync()).Where(item =>
            item.Identifier == identifier && item.Timestamp > startTime.UtcDateTime && item.Timestamp < endTime.UtcDateTime)
            .OrderByDescending(item => item.Timestamp).ToArray();
        }
        return Array.Empty<T>();
    }
    string? URL { get; init; }
    string? UserName { get; init; }
    string? Password { get; init; }
    string? Organize { get; init; }
}