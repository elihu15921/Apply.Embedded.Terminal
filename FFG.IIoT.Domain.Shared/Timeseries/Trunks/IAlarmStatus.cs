namespace IIoT.Domain.Shared.Timeseries.Trunks;
public interface IAlarmStatus : ISequelExpert<IAlarmStatus.Entity>
{
    ValueTask InsertAsync(Entity entity);

    [Measurement("part_status")]
    sealed class Entity : MetaBase
    {
        [Column("door_interlock")] public required byte DoorInterlock { get; init; }
        [Column("spindle_judgment_no_tool")] public required byte SpindleJudgmentNoTool { get; init; }
        [Column("rotary_table_overheat")] public required byte RotaryTableOverheat { get; init; }
        [Column("motor_overload")] public required byte MotorOverload { get; init; }
        [Column("air_pressure_alarm")] public required byte AirPressureAlarm { get; init; }
        [Column("spindle_cooler_alarm")] public required byte SpindleCoolerAlarm { get; init; }
        [Column("lube_alarm")] public required byte LubeAlarm { get; init; }
        [Column("lube_pressure_alarm")] public required byte LubePressureAlarm { get; init; }
        [Column("coolant_tank_high")] public required byte CoolantTankHigh { get; init; }
        [Column("coolant_tank_low")] public required byte CoolantTankLow { get; init; }
    }
}