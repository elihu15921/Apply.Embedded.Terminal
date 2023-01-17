namespace IIoT.Infrastructure.Facilities;
public sealed class MainProfile
{
    [YamlMember(ApplyNamingConventions = false)] public TextRoot Root { get; init; } = new();
    [YamlMember(ApplyNamingConventions = false)] public TextControl Control { get; init; } = new();
    public sealed class TextRoot
    {
        [YamlMember(ApplyNamingConventions = false)] public bool Debug { get; init; }
        [YamlMember(ApplyNamingConventions = false)] public string Language { get; init; } = NeutralUtility.Language;
    }
    public sealed class TextControl
    {
        [YamlMember(ApplyNamingConventions = false)] public HostType Type { get; init; } = HostType.Mitsubishi;
        [YamlMember(ApplyNamingConventions = false)] public string Address { get; init; } = IPAddress.Loopback.ToString();
    }
}