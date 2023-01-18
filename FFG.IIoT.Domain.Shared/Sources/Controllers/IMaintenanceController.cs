namespace IIoT.Domain.Shared.Sources.Controllers;
public interface IMaintenanceController
{
    void SetMitsubishiWeekly(MitsubishiInterval[] intervals);
    void SetMitsubishiMonthly(MitsubishiInterval[] intervals);
    void SetMitsubishiHalfYear(MitsubishiInterval[] intervals);

    [StructLayout(LayoutKind.Auto)]
    readonly record struct MitsubishiInterval
    {
        public required int ItemNo { get; init; }
        public required int CumulativeDay { get; init; }
    }
    MitsubishiInterval[] MitsubishiWeeklies { get; }
    MitsubishiInterval[] MitsubishiMonthlies { get; }
    MitsubishiInterval[] MitsubishiHalfYears { get; }
}