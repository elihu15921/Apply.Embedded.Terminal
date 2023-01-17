﻿using static IIoT.Domain.Shared.Sources.IMaintenanceSource;

namespace IIoT.Domain.Sources;
internal sealed class MaintenanceSource : IMaintenanceSource
{
    MitsubishiInterval[] _mitsubishiWeekly = Array.Empty<MitsubishiInterval>();
    MitsubishiInterval[] _mitsubishiMonthly = Array.Empty<MitsubishiInterval>();
    MitsubishiInterval[] _mitsubishiHalfYear = Array.Empty<MitsubishiInterval>();
    public void SetMitsubishiWeekly(MitsubishiInterval[] intervals) => _mitsubishiWeekly = intervals;
    public void SetMitsubishiMonthly(MitsubishiInterval[] intervals) => _mitsubishiMonthly = intervals;
    public void SetMitsubishiHalfYear(MitsubishiInterval[] intervals) => _mitsubishiHalfYear = intervals;
    public MitsubishiInterval[] MitsubishiWeeklies { get { return _mitsubishiWeekly; } }
    public MitsubishiInterval[] MitsubishiMonthlies { get { return _mitsubishiMonthly; } }
    public MitsubishiInterval[] MitsubishiHalfYears { get { return _mitsubishiHalfYear; } }
}