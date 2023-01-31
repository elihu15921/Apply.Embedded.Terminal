namespace IIoT.Station.Apis.Foundations;

[ApiExplorerSettings(GroupName = nameof(Foundations))]
public class Manages : ControllerBase
{
    [HttpGet(Name = nameof(GetMaintenance))]
    public IActionResult GetMaintenance()
    {//Maintenances
        using (CultureHelper.Use(Language))
        {
            try
            {
                return Ok(Turbo.Mitsubishi.GetMaintenance());
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
    public required ITurboWrapper Turbo { get; init; }
}