namespace IIoT.Station.Apis.Components;

[ApiExplorerSettings(GroupName = nameof(Components))]
public class Spindles : ControllerBase
{
    [HttpGet("lifespans", Name = nameof(GetSpindleLifespan))]
    public IActionResult GetSpindleLifespan()
    {
        using (CultureHelper.Use(Menu.Language))
        {
            try
            {
                List<LifespanRow.Speed> speeds = new();
                foreach (var latest in Latest.LifespanSpeeds)
                {
                    var (number, description) = GetDescription<ILifespanSpeed.Entity.SpeedRange>().First(item => item.Key == latest.Range).Value;
                    speeds.Add(new()
                    {
                        RangeNo = number,
                        Hour = latest.Hour,
                        Minute = latest.Minute,
                        Second = latest.Second,
                        Description = description
                    });
                }
                return Ok(new LifespanRow { Speeds = speeds });
            }
            catch (Exception e)
            {
                return NotFound(new { e.Message });
            }
        }
    }
    public readonly record struct LifespanRow
    {
        public required IEnumerable<Speed> Speeds { get; init; }
        public readonly record struct Speed
        {
            public required int RangeNo { get; init; }
            public required int Hour { get; init; }
            public required int Minute { get; init; }
            public required int Second { get; init; }
            public required string Description { get; init; }
        }
    }
    public required ILatestPool Latest { get; init; }
}