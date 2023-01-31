﻿namespace IIoT.Station.Apis.Foundations;

[ApiExplorerSettings(GroupName = nameof(Foundations))]
public class Trunks : ControllerBase
{
    [HttpGet("informations", Name = nameof(GetTrunkInformation))]
    public IActionResult GetTrunkInformation()
    {
        using (CultureHelper.Use(Language))
        {
            try
            {
                return Ok(new Row
                {
                    Ecomode = default,
                    MachineStatus = Latest.BasicInformation?.Status ?? default
                });
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }

    [StructLayout(LayoutKind.Auto)]
    public readonly record struct Row
    {
        public required bool Ecomode { get; init; }
        public required int MachineStatus { get; init; }
    }
    public required ILatestPool Latest { get; init; }
}