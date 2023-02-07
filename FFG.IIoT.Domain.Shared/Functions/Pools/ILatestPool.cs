namespace IIoT.Domain.Shared.Functions.Pools;
public interface ILatestPool
{
    #region Foundations
    void Push(in IBasiceInformation.Entity entity);
    void Push(in IPartStatus.Entity entity);
    IBasiceInformation.Entity? BasiceInformation { get; }
    IPartStatus.Entity? PartStatus { get; }
    #endregion

    #region Manages
    void Push(in IMaintenanceWeekly.Entity[] entities);
    void Push(in IMaintenanceMonthly.Entity[] entities);
    void Push(in IMaintenanceYear.Entity[] entities);
    IMaintenanceWeekly.Entity[] MaintenanceWeeklies { get; }
    IMaintenanceMonthly.Entity[] MaintenanceMonthlies { get; }
    IMaintenanceYear.Entity[] MaintenanceYears { get; }
    #endregion

    #region Components
    void Push(in ILifespanSpeed.Entity[] entities);
    void Push(in IThermalCompensation.Entity entity);
    ILifespanSpeed.Entity[] LifespanSpeeds { get; }
    IThermalCompensation.Entity? ThermalCompensation { get; }
    #endregion

    #region Externals
    void Push(in ITangramConnection.Entity entity);
    ITangramConnection.Entity? TangramConnection { get; }
    #endregion
}