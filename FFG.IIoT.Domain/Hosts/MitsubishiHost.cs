﻿using static IIoT.Domain.Shared.Hosts.IMitsubishiHost;

namespace IIoT.Domain.Hosts;
internal sealed class MitsubishiHost : IMitsubishiHost
{
    readonly IBasicFunction _basicFunction;
    public MitsubishiHost(IBasicFunction basicFunction) => _basicFunction = basicFunction;
    public async ValueTask CreateAsync(IPAddress address)
    {
        await Warship.ConnectAsync(new IPEndPoint(address, Port));
        await InfrastructureAsync(150, 4, DeviceCode.D);
        {
            Warship.Shutdown(SocketShutdown.Both);
            Warship.Close();
            Warship.Dispose();
        }
    }
    async ValueTask InfrastructureAsync(int serialNo, int length, DeviceCode code)
    {
        await Warship.SendAsync(HexBytes(new ReadText
        {
            Fixed = FixedPart.FixedHead,
            Timer = FixedPart.DataTimer,
            Length = FixedPart.DataLength,
            Command = FixedPart.ReadCommand,
            SubCommand = FixedPart.MultiPoint,
            DeviceCode = code.GetDescription(),
            StartPoint = _basicFunction.ConvertHEX(serialNo, WordLength.Three),
            Quantity = _basicFunction.ConvertHEX(length, WordLength.Two)
        }));
        var buffers = _basicFunction.BytePool.Rent(32);
        foreach (var receive in Capture(BitConverter.ToString(buffers, default, await Warship.ReceiveAsync(buffers))))
        {

        }
        _basicFunction.BytePool.Return(buffers);
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
    Socket Warship { get; } = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
}