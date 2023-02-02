namespace IIoT.Domain.Functions.Pools;
internal sealed class LatestPool : ILatestPool
{
    #region Foundations
    public void Push(IBasicInformation.Entity entity) => BasicInformation = entity;
    public IBasicInformation.Entity? BasicInformation { get; private set; }
    #endregion

    #region Manages
    public void Push(IMaintenanceWeekly.Entity[] entities) => MaintenanceWeeklies = entities;
    public void Push(IMaintenanceMonthly.Entity[] entities) => MaintenanceMonthlies = entities;
    public void Push(IMaintenanceYear.Entity[] entities) => MaintenanceYears = entities;
    public IMaintenanceWeekly.Entity[] MaintenanceWeeklies { get; private set; } = Array.Empty<IMaintenanceWeekly.Entity>();
    public IMaintenanceMonthly.Entity[] MaintenanceMonthlies { get; private set; } = Array.Empty<IMaintenanceMonthly.Entity>();
    public IMaintenanceYear.Entity[] MaintenanceYears { get; private set; } = Array.Empty<IMaintenanceYear.Entity>();
    #endregion

    #region Components
    public void Push(ILifespanSpeed.Entity[] entities) => LifespanSpeeds = entities;
    public ILifespanSpeed.Entity[] LifespanSpeeds { get; private set; } = Array.Empty<ILifespanSpeed.Entity>();
    #endregion

    #region Externals
    public void Push(ITangramConnection.Entity entity) => TangramConnection = entity;
    public ITangramConnection.Entity? TangramConnection { get; private set; }
    #endregion
}