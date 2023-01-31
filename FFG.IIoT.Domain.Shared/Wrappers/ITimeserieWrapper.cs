namespace IIoT.Domain.Shared.Wrappers;
public interface ITimeserieWrapper
{
    enum BucketType
    {
        [Description("base_trunks")] Trunk = 101,
        [Description("base_manages")] Manage = 102,
        [Description("part_spindles")] Spindle = 201
    }
    IBasicInformation BasicInformation { get; }
    IMaintenanceWeekly MaintenanceWeekly { get; }
    IMaintenanceMonthly MaintenanceMonthly { get; }
    IMaintenanceYear MaintenanceYear { get; }
    ILifespanSpeed LifespanSpeed { get; }
}