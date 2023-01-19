namespace IIoT.Station.Apis.Foundations;

[ApiExplorerSettings(GroupName = nameof(Foundations))]
public class Maintenances : ControllerBase
{
    [HttpGet(Name = nameof(GetMaintenance))]
    public IActionResult GetMaintenance()
    {
        using (CultureHelper.Use(Language))
        {
            try
            {
                return Ok(Host.Mitsubishi.GetMaintenance());
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
    public required IHostWrapper Host { get; init; }
}