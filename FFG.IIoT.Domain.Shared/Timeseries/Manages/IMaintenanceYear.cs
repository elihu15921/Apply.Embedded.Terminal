namespace IIoT.Domain.Shared.Timeseries.Manages;
public interface IMaintenanceYear : ISequelExpert<IMaintenanceYear.Entity>
{
    ValueTask InsertAsync(Entity[] entities);

    [Measurement("maintenance_years")]
    sealed class Entity : MetaBase
    {
        [Column("serial_no", IsTag = true)] public required string SerialNo { get; init; }
        [Column("cumulative_day")] public required int CumulativeDay { get; init; }
    }
}