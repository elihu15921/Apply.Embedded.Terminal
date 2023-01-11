using static IIoT.Domain.Shared.Summits.IMitsubishiSummit;

namespace IIoT.Domain.Summits;
internal sealed class MitsubishiSummit : IMitsubishiSummit
{
    public MitsubishiSummit() => Pool = ArrayPool<byte>.Shared;
    public async ValueTask InfrastructureAsync(int serialNo, int length, DeviceCode code)
    {
        await Warship.SendAsync(HexBytes(new ReadText
        {
            Fixed = FixedPart.FixedHead,
            Timer = FixedPart.DataTimer,
            Length = FixedPart.DataLength,
            Command = FixedPart.ReadCommand,
            SubCommand = FixedPart.MultiPoint,
            DeviceCode = code.GetDescription(),
            StartPoint = Basic.ConvertHEX(serialNo, IBasicFunction.WordLength.Three),
            Quantity = Basic.ConvertHEX(length, IBasicFunction.WordLength.Two)
        }));
        var buffers = Pool.Rent(32);
        foreach (var receive in Capture(BitConverter.ToString(buffers, default, await Warship.ReceiveAsync(buffers))))
        {

        }
        Pool.Return(buffers);
    }
    static byte[] HexBytes(ReadText text)
    {
        var tag = $"{text.Fixed}{text.Length}{text.Timer}{text.Command}{text.SubCommand}{text.StartPoint}{text.DeviceCode}{text.Quantity}";
        var results = new byte[tag.Length / 2];
        {
            for (int item = default; item < results.Length; item++) results[item] = Convert.ToByte(tag.Substring(item * 2, 2).Trim(), 16);
            return results;
        }
    }
    static IEnumerable<int> Capture(string receive)
    {
        var low = string.Empty;
        var bytes = receive.Split('-').Skip(11).ToArray();
        for (int item = default; item < bytes.Length; item++)
        {
            if (item % 2 is 0) low = bytes[item];
            else yield return Convert.ToInt32($"{bytes[item]}{low}", 16);
        }
    }
    public required Socket Warship { get; set; }
    public required ArrayPool<byte> Pool { get; init; }
    public required IBasicFunction Basic { get; init; }
}