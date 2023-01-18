using static IIoT.Domain.Shared.Sources.Controllers.IInformationController;

namespace IIoT.Domain.Sources.Controllers;
internal sealed class InformationController : IInformationController
{
    MitsubishiInformationFoot _mitsubishiInformation;
    public void SetMitsubishi(MitsubishiInformationFoot foot) => _mitsubishiInformation = foot;
    public MitsubishiInformationFoot MitsubishiInformation { get { return _mitsubishiInformation; } }
}