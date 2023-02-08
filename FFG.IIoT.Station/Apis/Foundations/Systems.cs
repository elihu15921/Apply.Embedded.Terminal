namespace IIoT.Station.Apis.Foundations;

[ApiExplorerSettings(GroupName = nameof(Foundations))]
public class Systems : ControllerBase
{
    [HttpGet("runners", Name = nameof(GetSystemRunner))]
    public IActionResult GetSystemRunner()
    {
        using (CultureHelper.Use(Local.Language))
        {
            try
            {
                return Ok(new RunnerRow
                {
                    Basic = System.BasicRunnerTimestamp,
                    Host = System.HostRunnerTimestamp
                });
            }
            catch (Exception e)
            {
                return NotFound(new { e.Message });
            }
        }
    }

    [StructLayout(LayoutKind.Auto)]
    public readonly record struct RunnerRow
    {
        public required DateTime Basic { get; init; }
        public required DateTime Host { get; init; }
    }
    public required ISystemPool System { get; init; }
}