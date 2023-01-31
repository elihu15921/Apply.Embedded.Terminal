namespace IIoT.Domain.Shared.Timeseries.Manages;
public interface IMaintenanceMonthly : ISequelExpert<IMaintenanceMonthly.Entity>
{
    ValueTask InsertAsync(Entity[] entities);

    [Measurement("maintenance_monthlies")]
    sealed class Entity : MetaBase
    {
        [Column("serial_no", IsTag = true)] public required string SerialNo { get; init; }
        [Column("cumulative_day")] public required int CumulativeDay { get; init; }
    }
}