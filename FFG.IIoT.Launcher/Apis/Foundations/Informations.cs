namespace FFG.IIoT.Launcher.Apis.Foundations;

[ApiExplorerSettings(GroupName = nameof(Foundations))]
public class Informations : ControllerBase
{
    [HttpGet(Name = nameof(GetInformationAsync))]
    public async ValueTask<IActionResult> GetInformationAsync()
    {
        try
        {
            return Ok(new
            {
                Status = 0
            });
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}