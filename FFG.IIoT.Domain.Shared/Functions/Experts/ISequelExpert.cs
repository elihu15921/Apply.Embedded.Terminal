namespace IIoT.Domain.Shared.Functions.Experts;
public interface ISequelExpert<T> where T : ISequelExpert<T>.MetaBase
{
    ValueTask WriteAsync(T meta, string bucket);
    ValueTask WriteAsync(T[] metas, string bucket);
    T[] Read(in string bucket, string identifier, DateTimeOffset startTime, DateTimeOffset endTime);
    abstract class MetaBase
    {
        [Column(IsTimestamp = true)] public required DateTime Timestamp { get; init; }
        [Column("identifier", IsTag = true)] public required string Identifier { get; init; }
    }
}