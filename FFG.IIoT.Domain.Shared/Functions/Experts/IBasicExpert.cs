namespace IIoT.Domain.Shared.Functions.Experts;
public interface IBasicExpert
{
    ValueTask InitialProfileAsync();
    ValueTask InitialPoolAsync(string url, string organize, string username, string password, string bucket);
    string ConvertHEX(in int quantity, in WordLength length);
    enum WordLength
    {
        Two,
        Three
    }
    ref struct HistoryFoot
    {
        public static int RetentionDay => 14;
        public static int DayToSeconds => 86400;
        public static string Title => "[{0}] {1}";
        public static string DateFormat => "yyyy/MM/dd HH:mm:ss";
        public static string Location => Path.Combine(RootDirectory, "..", "Logs");
    }
    MainProfile? Profile { get; set; }
    MqttServer Transport { get; init; }
    ArrayPool<byte> BytePool { get; }
}