namespace IIoT.Domain.Shared.Timeseries.Universals;
public interface ITangramConnection : ISequelExpert<ITangramConnection.Entity>
{
    ValueTask InsertAsync(Entity entity);

    [Measurement("tangram_connection")]
    sealed class Entity : MetaBase
    {
        public enum ConnectionStatus
        {
            Connected = 0,
            Disconnected = 1
        }
        [Column("tangram_id", IsTag = true)] public required string TangramId { get; init; }
        [Column("status")] public required int Status { get; init; }
        public readonly record struct Meta
        {
            public required string TangramId { get; init; }
            public required int Status { get; init; }
            public required string Time { get; init; }
        }
    }
}