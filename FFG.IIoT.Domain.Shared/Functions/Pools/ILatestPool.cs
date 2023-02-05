namespace IIoT.Domain.Shared.Functions.Pools;
public interface ILatestPool
{
    #region Foundations
    void Push(IBasiceInformation.Entity entity);
    void Push(IPartStatus.Entity entity);
    IBasiceInformation.Entity? BasiceInformation { get; }
    IPartStatus.Entity? PartStatus { get; }
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
    void Push(IThermalCompensation.Entity entity);
    ILifespanSpeed.Entity[] LifespanSpeeds { get; }
    IThermalCompensation.Entity? ThermalCompensation { get; }
    #endregion

    #region Externals
    void Push(ITangramConnection.Entity entity);
    ITangramConnection.Entity? TangramConnection { get; }
    #endregion
}