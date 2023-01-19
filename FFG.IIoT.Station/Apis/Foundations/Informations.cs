namespace IIoT.Station.Apis.Foundations;

[ApiExplorerSettings(GroupName = nameof(Foundations))]
public class Informations : ControllerBase
{
    [HttpGet(Name = nameof(GetInformation))]
    public IActionResult GetInformation()
    {
        using (CultureHelper.Use(Language))
        {
            try
            {
                return Ok(Host.Mitsubishi.GetInformation());
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
    public required IHostWrapper Host { get; init; }
}