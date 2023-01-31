namespace IIoT.Domain.Functions.Pools;
internal sealed class LatestPool : ILatestPool
{
    public void Push(IBasicInformation.Entity entity) => BasicInformation = entity;
    public void Push(IMaintenanceWeekly.Entity[] entities) => MaintenanceWeeklies = entities;
    public void Push(IMaintenanceMonthly.Entity[] entities) => MaintenanceMonthlies = entities;
    public void Push(IMaintenanceYear.Entity[] entities) => MaintenanceYears = entities;
    public void Push(ILifespanSpeed.Entity[] entities) => LifespanSpeeds = entities;
    public IBasicInformation.Entity? BasicInformation { get; private set; }
    public IMaintenanceWeekly.Entity[] MaintenanceWeeklies { get; private set; } = Array.Empty<IMaintenanceWeekly.Entity>();
    public IMaintenanceMonthly.Entity[] MaintenanceMonthlies { get; private set; } = Array.Empty<IMaintenanceMonthly.Entity>();
    public IMaintenanceYear.Entity[] MaintenanceYears { get; private set; } = Array.Empty<IMaintenanceYear.Entity>();
    public ILifespanSpeed.Entity[] LifespanSpeeds { get; private set; } = Array.Empty<ILifespanSpeed.Entity>();
}