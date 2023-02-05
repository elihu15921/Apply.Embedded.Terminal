namespace IIoT.Domain.Shared.Timeseries.Spindles;
public interface IThermalCompensation : ISequelExpert<IThermalCompensation.Entity>
{
    ValueTask InsertAsync(Entity entity);

    [Measurement("thermal_compensations")]
    sealed class Entity : MetaBase
    {        
        [Column("thermal_first")] public required float ThermalFirst { get; init; }
        [Column("thermal_second")] public required float ThermalSecond { get; init; }
        [Column("thermal_third")] public required float ThermalThird { get; init; }
        [Column("thermal_fourth")] public required float ThermalFourth { get; init; }
        [Column("thermal_fifth")] public required float ThermalFifth { get; init; }
        [Column("compensation_x")] public required float CompensationX { get; init; }
        [Column("compensation_y")] public required float CompensationY { get; init; }
        [Column("compensation_z")] public required float CompensationZ { get; init; }
    }
}