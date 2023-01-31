namespace IIoT.Domain.Shared.Timeseries.Trunks;
public interface IBasicInformation : ISequelExpert<IBasicInformation.Entity>
{
    ValueTask InsertAsync(Entity entity);

    [Measurement("informations")]
    sealed class Entity : MetaBase
    {
        public enum MachineStatus
        {
            Idle = 0,
            Run = 1,
            Error = 2,
            Repair = 3
        }
        [Column("status")] public required int Status { get; init; }
    }
}