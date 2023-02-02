namespace IIoT.Domain.Shared.Functions.Pools;
public interface ILatestPool
{
    #region Foundations
    void Push(IBasicInformation.Entity entity);
    IBasicInformation.Entity? BasicInformation { get; }
    #endregion

    #region Manages
    void Push(IMaintenanceWeekly.Entity[] entities);
    void Push(IMaintenanceMonthly.Entity[] entities);
    void Push(IMaintenanceYear.Entity[] entities);
    IMaintenanceWeekly.Entity[] MaintenanceWeeklies { get; }
    IMaintenanceMonthly.Entity[] MaintenanceMonthlies { get; }
    IMaintenanceYear.Entity[] MaintenanceYears { get; }
    #endregion

    #region Components
    void Push(ILifespanSpeed.Entity[] entities);
    ILifespanSpeed.Entity[] LifespanSpeeds { get; }
    #endregion

    #region Externals
    void Push(ITangramConnection.Entity entity);
    ITangramConnection.Entity? TangramConnection { get; }
    #endregion
}