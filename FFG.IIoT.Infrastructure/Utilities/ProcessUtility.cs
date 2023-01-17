namespace IIoT.Infrastructure.Utilities;
public static class ProcessUtility
{
    public enum HostType
    {
        None = 0,
        Mitsubishi = 1
    }
    public enum MachineStatus
    {
        Idle = 0,
        Run = 1,
        Error = 2,
        Repair = 3
    }
    public enum PartType
    {
        None = 0,
        Spindle = 1
    }
}