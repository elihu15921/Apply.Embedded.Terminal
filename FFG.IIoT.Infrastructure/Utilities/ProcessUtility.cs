namespace IIoT.Infrastructure.Utilities;
public static class ProcessUtility
{
    public const string FFG = "ffg";
    public const string Passkey = "12345678";
    public interface IEntranceTrigger
    {
        ValueTask BuildAsync();
    }
    public enum HostType
    {
        None = 0,
        Mitsubishi = 1,
        Fanuc = 2,
        Siemens = 3
    }
    public enum LanguageType
    {
        [Description("en-US")] English,
        [Description("zh-CN")] Simplified,
        [Description("zh-TW")] Traditional
    }
    public ref struct Point
    {
        public static int Broker => 1883;
        public static int Metadata => 8086;
        public static int Entrance => 17770;
    }
    public ref struct Menu
    {
        public static int Timeout => 30;
        public static TimeSpan RefreshTime => TimeSpan.FromSeconds(1);
        public static string RootDirectory => Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? string.Empty;
        public static string Language { get; set; } = LanguageType.English.GetDescription();
    }
}