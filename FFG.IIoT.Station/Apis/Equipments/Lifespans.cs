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
                    PartType.Spindle => Ok(Turbo.Mitsubishi.GetSpindleLife()),
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

    [StructLayout(LayoutKind.Auto)]
    public readonly record struct SpindleRow
    {
        public required int RangeNo { get; init; }
        public required int Hour { get; init; }
        public required int Minute { get; init; }
        public required int Second { get; init; }
        public required string Description { get; init; }
    }
    public required IStringLocalizer<Fielder> Fielder { get; init; }
    public required ITurboWrapper Turbo { get; init; }
}