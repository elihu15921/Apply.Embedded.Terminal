namespace IIoT.Station.Apis.Foundations;

[ApiExplorerSettings(GroupName = nameof(Foundations))]
public class Trunks : ControllerBase
{
    [HttpGet("informations", Name = nameof(GetTrunkInformation))]
    public IActionResult GetTrunkInformation()
    {
        using (CultureHelper.Use(Menu.Language))
        {
            try
            {
                return Ok(new InformationRow
                {
                    MachineStatus = Latest.BasiceInformation?.Status ?? default,
                    Ecomode = Latest.PartStatus?.Ecomode is 1,
                    CuttingFluidMotor = Latest.PartStatus?.CuttingFluidMotor is 1,
                    ChassisCleanerMotor = Latest.PartStatus?.ChassisCleanerMotor is 1,
                    ChipRemovalMotor = Latest.PartStatus?.ChipRemovalMotor is 1,
                    ChipRemovalBackwashMotor = Latest.PartStatus?.ChipRemovalBackwashMotor is 1,
                    CoolantThroughSpindleMotor = Latest.PartStatus?.CoolantThroughSpindleMotor is 1,
                    PumpMotor = Latest.PartStatus?.PumpMotor is 1
                });
            }
            catch (Exception e)
            {
                return NotFound(new { e.Message });
            }
        }
    }

    [StructLayout(LayoutKind.Auto)]
    public readonly record struct InformationRow
    {
        public required int MachineStatus { get; init; }
        public required bool Ecomode { get; init; }
        public required bool CuttingFluidMotor { get; init; }
        public required bool ChassisCleanerMotor { get; init; }
        public required bool ChipRemovalMotor { get; init; }
        public required bool ChipRemovalBackwashMotor { get; init; }
        public required bool CoolantThroughSpindleMotor { get; init; }
        public required bool PumpMotor { get; init; }
    }
    public required ILatestPool Latest { get; init; }
}