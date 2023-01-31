namespace IIoT.Domain.Shared.Timeseries.Manages;
public interface IMaintenanceWeekly : ISequelExpert<IMaintenanceWeekly.Entity>
{
    ValueTask InsertAsync(Entity[] entities);

    [Measurement("maintenance_weeklies")]
    sealed class Entity : MetaBase
    {
        [Column("serial_no", IsTag = true)] public required string SerialNo { get; init; }
        [Column("cumulative_day")] public required int CumulativeDay { get; init; }
    }
}