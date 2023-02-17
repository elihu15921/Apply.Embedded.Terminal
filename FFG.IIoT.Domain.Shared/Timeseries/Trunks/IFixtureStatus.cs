namespace IIoT.Domain.Shared.Timeseries.Trunks;
public interface IFixtureStatus : ISequelExpert<IFixtureStatus.Entity>
{
    ValueTask InsertAsync(Entity entity);

    [Measurement("fixture_status")]
    sealed class Entity : MetaBase
    {
        [Column("spindle_clamp")] public required byte SpindleClamp { get; init; }
        [Column("machine_clamp")] public required byte MachineClamp { get; init; }
        [Column("machine_unclamp")] public required byte MachineUnclamp { get; init; }
        [Column("tool_unclamp")] public required byte ToolUnclamp { get; init; }
        [Column("tool_clamp")] public required byte ToolClamp { get; init; }
        [Column("tool_manual_relaxation")] public required byte ToolManualRelaxation { get; init; }
        [Column("tool_set_upper")] public required byte ToolSetUpper { get; init; }
        [Column("tool_set_lower")] public required byte ToolSetLower { get; init; }
        [Column("tool_magazine_counter")] public required byte ToolMagazineCounter { get; init; }
        [Column("arm_zero_point")] public required byte ArmZeroPoint { get; init; }
        [Column("arm_stop_point")] public required byte ArmStopPoint { get; init; }
        [Column("arm_point_60_degrees")] public required byte ArmPoint60Degrees { get; init; }
        [Column("arm_point_180_degrees")] public required byte ArmPoint180Degrees { get; init; }
    }
}