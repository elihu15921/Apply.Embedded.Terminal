namespace IIoT.Domain.Shared.Wrappers;
public interface ITimeserieWrapper
{
    enum BucketTag
    {
        [Description("base_trunks")] Trunk = 101,
        [Description("base_manages")] Manage = 102,
        [Description("part_spindles")] Spindle = 201,
        [Description("external_tangrams")] Tangram = 301
    }

    #region Manages
    IMaintenanceWeekly MaintenanceWeekly { get; }
    IMaintenanceMonthly MaintenanceMonthly { get; }
    IMaintenanceYear MaintenanceYear { get; }
    #endregion

    #region Spindles
    ILifespanSpeed LifespanSpeed { get; }
    #endregion

    #region Trunks
    IBasicInformation BasicInformation { get; }
    #endregion

    #region Universals
    ITangramConnection TangramConnection { get; }
    #endregion
}