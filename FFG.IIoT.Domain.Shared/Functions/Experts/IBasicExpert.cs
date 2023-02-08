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
    MainProfile? Profile { get; set; }
    MqttServer Transport { get; init; }
    ArrayPool<byte> BytePool { get; }
}