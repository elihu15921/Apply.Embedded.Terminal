namespace IIoT.Domain.Shared.Divisions.Metas.Spindles;
public interface ILifespanSpindle : ISequelExpert<ILifespanSpindle.Entity>
{



    [Measurement("lifespan")]
    sealed class Entity : MetaBase
    {
        [Column("range_no")] public required int RangeNo { get; init; }
        [Column("hour")] public required int Hour { get; init; }
        [Column("minute")] public required int Minute { get; init; }
        [Column("second")] public required int Second { get; init; }
    }
}