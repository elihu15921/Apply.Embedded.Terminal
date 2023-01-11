namespace IIoT.Infrastructure.Facilities;
public sealed class YamlSource : FileConfigurationSource
{
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        EnsureDefaults(builder);
        return new YamlProvider(this);
    }
}