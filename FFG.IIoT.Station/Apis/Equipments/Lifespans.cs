namespace IIoT.Station.Apis.Equipments;

[ApiExplorerSettings(GroupName = nameof(Equipments))]
public class Lifespans : ControllerBase
{
    [HttpGet(Name = nameof(GetLifespan))]
    public IActionResult GetLifespan([FromQuery] Query query)
    {
        using (CultureHelper.Use(Language))
        {
            try
            {
                return query.Part switch
                {
                    PartType.Spindle => Ok(Host.Mitsubishi.GetSpindleLife()),
                    _ => throw new Exception(Fielder["field.value.mismatch", nameof(PartType.Spindle)])
                };
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
    public sealed class Query
    {
        public required PartType Part { get; init; }
    }
    public required IStringLocalizer<Fielder> Fielder { get; init; }
    public required IHostWrapper Host { get; init; }
}