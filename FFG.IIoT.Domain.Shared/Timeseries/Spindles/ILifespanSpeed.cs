namespace IIoT.Domain.Shared.Timeseries.Spindles;
public interface ILifespanSpeed : ISequelExpert<ILifespanSpeed.Entity>
{
    ValueTask InsertAsync(Entity[] entities);

    [Measurement("lifespans")]
    sealed class Entity : MetaBase
    {
        public enum SpeedRange
        {
            [Description("1~2000RPM")] A = 1,
            [Description("2001~4000RPM")] B = 2,
            [Description("4001~6000RPM")] C = 3,
            [Description("6001~8000RPM")] D = 4,
            [Description("8001~10000RPM")] E = 5,
            [Description("10001~12000RPM")] F = 6,
            [Description("12001~14000RPM")] G = 7,
            [Description("14001~16000RPM")] H = 8,
            [Description("16001~18000RPM")] I = 9,
            [Description("18001~20000RPM")] J = 10,
            [Description("20001~22000RPM")] K = 11,
            [Description("22001~24000RPM")] L = 12,
            [Description("24001~26000RPM")] M = 13,
            [Description("26001~28000RPM")] N = 14,
            [Description("28001~30000RPM")] O = 15
        }
        [Column("range", IsTag = true)] public required string Range { get; init; }
        [Column("hour")] public required int Hour { get; init; }
        [Column("minute")] public required int Minute { get; init; }
        [Column("second")] public required int Second { get; init; }
    }
}