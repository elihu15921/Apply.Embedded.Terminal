namespace IIoT.Domain.Shared.Functions.Experts;
public interface ISequelExpert<T> where T : ISequelExpert<T>.MetaBase
{
    const int Day = 86400;
    ValueTask WriteAsync(T meta, string bucket);
    ValueTask WriteAsync(T[] metas, string bucket);
    abstract class MetaBase
    {
        [Column(IsTimestamp = true)] public required DateTime Timestamp { get; init; }
    }
}