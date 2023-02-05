namespace IIoT.Domain.Shared.Timeseries.Trunks;
public interface IPartStatus : ISequelExpert<IPartStatus.Entity>
{
    ValueTask InsertAsync(Entity entity);

    [Measurement("part_status")]
    sealed class Entity : MetaBase
    {
        [Column("eco_mode")] public required byte Ecomode { get; init; }
        [Column("cutting_fluid_motor")] public required byte CuttingFluidMotor { get; init; }
        [Column("chassis_cleaner_motor")] public required byte ChassisCleanerMotor { get; init; }
        [Column("chip_removal_motor")] public required byte ChipRemovalMotor { get; init; }
        [Column("chip_removal_backwash_motor")] public required byte ChipRemovalBackwashMotor { get; init; }
        [Column("coolant_through_spindle_motor")] public required byte CoolantThroughSpindleMotor { get; init; }
        [Column("pump_motor")] public required byte PumpMotor { get; init; }
    }
}