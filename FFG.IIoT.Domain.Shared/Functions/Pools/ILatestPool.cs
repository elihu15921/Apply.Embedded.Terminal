namespace IIoT.Domain.Shared.Functions.Pools;
public interface ILatestPool
{
    void Push(IBasicInformation.Entity entity);
    void Push(IMaintenanceWeekly.Entity[] entities);
    void Push(IMaintenanceMonthly.Entity[] entities);
    void Push(IMaintenanceYear.Entity[] entities);
    void Push(ILifespanSpeed.Entity[] entities);
    IBasicInformation.Entity? BasicInformation { get; }
    IMaintenanceWeekly.Entity[] MaintenanceWeeklies { get; }
    IMaintenanceMonthly.Entity[] MaintenanceMonthlies { get; }
    IMaintenanceYear.Entity[] MaintenanceYears { get; }
    ILifespanSpeed.Entity[] LifespanSpeeds { get; }
}