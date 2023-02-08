namespace IIoT.Infrastructure.Facilities;
public sealed class MainProfile
{
    [YamlMember(ApplyNamingConventions = false)] public TextRoot Root { get; init; } = new();
    [YamlMember(ApplyNamingConventions = false)] public TextControl Control { get; init; } = new();
    [YamlMember(ApplyNamingConventions = false)] public TextPool Pool { get; init; } = new();
    public sealed class TextRoot
    {
        [YamlMember(ApplyNamingConventions = false)] public bool Debug { get; init; }
        [YamlMember(ApplyNamingConventions = false)] public string Language { get; init; } = Local.Language;
    }
    public sealed class TextControl
    {
        [YamlMember(ApplyNamingConventions = false)] public HostType Type { get; init; } = HostType.Mitsubishi;
        [YamlMember(ApplyNamingConventions = false)] public string IP { get; init; } = IPAddress.Loopback.ToString();
    }
    public sealed class TextPool
    {
        [YamlMember(ApplyNamingConventions = false)] public string URL { get; init; } = $"{Uri.UriSchemeHttp}://{IPAddress.Loopback}:{Local.Metadata}";
        [YamlMember(ApplyNamingConventions = false)] public string Organize { get; init; } = Label.Organize;
        [YamlMember(ApplyNamingConventions = false)] public string Username { get; init; } = Label.Username;
        [YamlMember(ApplyNamingConventions = false)] public string Password { get; init; } = Label.Passkey.UseDecryptAES();
    }
}