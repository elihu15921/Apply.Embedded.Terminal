namespace IIoT.Domain.Shared.Functions;
public interface IBasicFunction
{
    const int RetainedFileCount = 14 * 24;
    const string LogPath = "logs/log-.log";
    ValueTask InitialProfile();
    string ConvertHEX(in int quantity, in WordLength length);
    enum WordLength
    {
        Two,
        Three
    }
    MainProfile? Profile { get; set; }
}