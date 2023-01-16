namespace FFG.IIoT.Launcher.Apis.Components;

[ApiExplorerSettings(GroupName = nameof(Components))]
public class Lifespans : ControllerBase
{
    [HttpGet(Name = nameof(GetLifespanAsync))]
    public async ValueTask<IActionResult> GetLifespanAsync()
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
    public sealed class Query : Satchel
    {
        public required Guid GroupId { get; init; }
        public required bool PushTaskFilter { get; init; }
    }
}