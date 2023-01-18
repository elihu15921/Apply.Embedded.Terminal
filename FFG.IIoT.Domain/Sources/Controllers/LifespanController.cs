using static IIoT.Domain.Shared.Sources.Controllers.ILifespanController;

namespace IIoT.Domain.Sources.Controllers;
internal sealed class LifespanController : ILifespanController
{
    MitsubishiSpindle[] _mitsubishiSpindles = Array.Empty<MitsubishiSpindle>();
    public void SetMitsubishiSpindle(MitsubishiSpindle[] spindles) => _mitsubishiSpindles = spindles;
    public MitsubishiSpindle[] MitsubishiSpindles { get { return _mitsubishiSpindles; } }
}