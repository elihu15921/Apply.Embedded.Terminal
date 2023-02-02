namespace IIoT.Station.Apis.Foundations;

[ApiExplorerSettings(GroupName = nameof(Foundations))]
public class Manages : ControllerBase
{
    [HttpGet("maintenances", Name = nameof(GetManageMaintenance))]
    public IActionResult GetManageMaintenance([FromQuery] Query query)
    {
        using (CultureHelper.Use(Menu.Language))
        {
            try
            {
                return query.Cycle switch
                {
                    CycleType.Weekly => Ok(new MaintenanceRow
                    {
                        Cycles = Latest.MaintenanceWeeklies.Select(item => new MaintenanceRow.Cycle
                        {
                            SerialNo = int.Parse(item.SerialNo),
                            CumulativeDay = item.CumulativeDay
                        })
                    }),
                    CycleType.Monthly => Ok(new MaintenanceRow
                    {
                        Cycles = Latest.MaintenanceMonthlies.Select(item => new MaintenanceRow.Cycle
                        {
                            SerialNo = int.Parse(item.SerialNo),
                            CumulativeDay = item.CumulativeDay
                        })
                    }),
                    CycleType.Year => Ok(new MaintenanceRow
                    {
                        Cycles = Latest.MaintenanceYears.Select(item => new MaintenanceRow.Cycle
                        {
                            SerialNo = int.Parse(item.SerialNo),
                            CumulativeDay = item.CumulativeDay
                        })
                    }),
                    _ => throw new Exception(Fielder["field.value.mismatch", nameof(CycleType)])
                };
            }
            catch (Exception e)
            {
                return NotFound(new { e.Message });
            }
        }
    }
    public enum CycleType
    {
        None = 0,
        Weekly = 1,
        Monthly = 2,
        Year = 3
    }
    public sealed class Query
    {
        public required CycleType Cycle { get; init; }
    }
    public readonly record struct MaintenanceRow
    {
        public required IEnumerable<Cycle> Cycles { get; init; }

        [StructLayout(LayoutKind.Auto)]
        public readonly record struct Cycle
        {
            public required int SerialNo { get; init; }
            public required int CumulativeDay { get; init; }
        }
    }
    public required IStringLocalizer<Fielder> Fielder { get; init; }
    public required ILatestPool Latest { get; init; }
}