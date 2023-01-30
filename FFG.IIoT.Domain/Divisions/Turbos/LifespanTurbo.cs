using static IIoT.Domain.Shared.Divisions.Turbos.ILifespanTurbo;

namespace IIoT.Domain.Divisions.Turbos;
internal sealed class LifespanTurbo : ILifespanTurbo
{
    MitsubishiSpindle[] _mitsubishiSpindles = Array.Empty<MitsubishiSpindle>();
    public void SetMitsubishiSpindle(MitsubishiSpindle[] spindles) => _mitsubishiSpindles = spindles;
    public MitsubishiSpindle[] MitsubishiSpindles { get { return _mitsubishiSpindles; } }
}