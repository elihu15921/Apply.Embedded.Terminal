namespace IIoT.Domain.Shared.Functions;
public interface IBasicFunction
{
    ValueTask InitialProfile();
    string ConvertHEX(in int quantity, in WordLength length);
    enum WordLength
    {
        Two,
        Three
    }
    ref struct HistoryFoot
    {
        public static int RetentionDay => 14;
        public static string Title => "[{0}] {1}";
        public static string Location => Path.Combine(NeutralUtility.RootDirectory, "..", "Logs");
    }
    MainProfile? Profile { get; set; }
    ArrayPool<byte> BytePool { get; }
}