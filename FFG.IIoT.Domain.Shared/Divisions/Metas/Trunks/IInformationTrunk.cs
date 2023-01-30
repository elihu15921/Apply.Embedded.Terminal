namespace IIoT.Domain.Shared.Divisions.Metas.Trunks;
public interface IInformationTrunk : ISequelExpert<IInformationTrunk.Entity>
{
    ValueTask BuildAsync();
    ValueTask InsertAsync(Entity entity);
    enum Status
    {
        Idle = 0,
        Run = 1,
        Error = 2,
        Repair = 3
    }

    [Measurement("information")]
    sealed class Entity : MetaBase
    {
        [Column("status")] public required int Status { get; init; }
    }
}