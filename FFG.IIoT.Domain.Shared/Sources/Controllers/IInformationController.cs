namespace IIoT.Domain.Shared.Sources.Controllers;
public interface IInformationController
{
    void SetMitsubishi(MitsubishiInformationFoot foot);
    public enum MachineStatus
    {
        Idle = 0,
        Run = 1,
        Error = 2,
        Repair = 3
    }

    [StructLayout(LayoutKind.Auto)]
    readonly record struct MitsubishiInformationFoot
    {
        public required bool Ecomode { get; init; }
        public required MachineStatus MachineStatus { get; init; }
    }
    MitsubishiInformationFoot MitsubishiInformation { get; }
}