using static IIoT.Domain.Shared.Hosts.IMitsubishiHost;
using static IIoT.Domain.Shared.Sources.IMaintenanceSource;

namespace IIoT.Domain.Hosts;
internal sealed class MitsubishiHost : IMitsubishiHost
{
    readonly IBasicFunction _basicFunction;
    readonly IMaintenanceSource _maintenanceSource;
    public MitsubishiHost(IBasicFunction basicFunction, IMaintenanceSource maintenanceSource)
    {
        _basicFunction = basicFunction;
        _maintenanceSource = maintenanceSource;
    }
    public async ValueTask CreateAsync(IPAddress address)
    {
        await Warship.ConnectAsync(new IPEndPoint(address, Port));
        await PushSpindleLifeAsync(CreateReader(9571, 29, DeviceCode.R));
        {
            Warship.Shutdown(SocketShutdown.Both);
            Warship.Close();
            Warship.Dispose();
        }
    }
    public Maintenance GetMaintenance() => new()
    {
        Weeklies = _maintenanceSource.MitsubishiWeeklies,
        Monthlies = _maintenanceSource.MitsubishiMonthlies,
        HalfYears = _maintenanceSource.MitsubishiHalfYears
    };
    async ValueTask PushSpindleLifeAsync(byte[] values)
    {
        await Warship.SendAsync(values);
        var buffers = _basicFunction.BytePool.Rent(128);
        var receives = Capture(BitConverter.ToString(buffers, default, await Warship.ReceiveAsync(buffers))).ToArray();
        {
            _maintenanceSource.SetMitsubishiWeekly(new[]
            {
                new MitsubishiInterval
                {
                    ItemNo = 1,
                    CumulativeDay = receives[0]
                },
                new MitsubishiInterval
                {
                    ItemNo = 2,
                    CumulativeDay = receives[1]
                },
                new MitsubishiInterval
                {
                    ItemNo = 3,
                    CumulativeDay = receives[2]
                },
                new MitsubishiInterval
                {
                    ItemNo = 4,
                    CumulativeDay = receives[3]
                },
                new MitsubishiInterval
                {
                    ItemNo = 5,
                    CumulativeDay = receives[4]
                },
                new MitsubishiInterval
                {
                    ItemNo = 6,
                    CumulativeDay = receives[5]
                },
                new MitsubishiInterval
                {
                    ItemNo = 7,
                    CumulativeDay = receives[6]
                },
                new MitsubishiInterval
                {
                    ItemNo = 8,
                    CumulativeDay = receives[7]
                },
                new MitsubishiInterval
                {
                    ItemNo = 9,
                    CumulativeDay = receives[8]
                }
            });
            _maintenanceSource.SetMitsubishiMonthly(new[]
            {
                new MitsubishiInterval
                {
                    ItemNo = 1,
                    CumulativeDay = receives[9]
                },
                new MitsubishiInterval
                {
                    ItemNo = 2,
                    CumulativeDay = receives[10]
                },
                new MitsubishiInterval
                {
                    ItemNo = 3,
                    CumulativeDay = receives[11]
                },
                new MitsubishiInterval
                {
                    ItemNo = 4,
                    CumulativeDay = receives[12]
                },
                new MitsubishiInterval
                {
                    ItemNo = 5,
                    CumulativeDay = receives[13]
                },
                new MitsubishiInterval
                {
                    ItemNo = 6,
                    CumulativeDay = receives[14]
                },
                new MitsubishiInterval
                {
                    ItemNo = 7,
                    CumulativeDay = receives[15]
                },
                new MitsubishiInterval
                {
                    ItemNo = 8,
                    CumulativeDay = receives[16]
                },
                new MitsubishiInterval
                {
                    ItemNo = 9,
                    CumulativeDay = receives[17]
                },
                new MitsubishiInterval
                {
                    ItemNo = 10,
                    CumulativeDay = receives[18]
                },
                new MitsubishiInterval
                {
                    ItemNo = 11,
                    CumulativeDay = receives[19]
                }
            });
            _maintenanceSource.SetMitsubishiHalfYear(new[]
            {
                new MitsubishiInterval
                {
                    ItemNo = 1,
                    CumulativeDay = receives[20]
                },
                new MitsubishiInterval
                {
                    ItemNo = 2,
                    CumulativeDay = receives[21]
                },
                new MitsubishiInterval
                {
                    ItemNo = 3,
                    CumulativeDay = receives[22]
                },
                new MitsubishiInterval
                {
                    ItemNo = 4,
                    CumulativeDay = receives[23]
                },
                new MitsubishiInterval
                {
                    ItemNo = 5,
                    CumulativeDay = receives[24]
                },
                new MitsubishiInterval
                {
                    ItemNo = 6,
                    CumulativeDay = receives[25]
                },
                new MitsubishiInterval
                {
                    ItemNo = 7,
                    CumulativeDay = receives[26]
                },
                new MitsubishiInterval
                {
                    ItemNo = 8,
                    CumulativeDay = receives[27]
                },
                new MitsubishiInterval
                {
                    ItemNo = 9,
                    CumulativeDay = receives[28]
                }
            });
        }
        _basicFunction.BytePool.Return(buffers);
    }
    byte[] CreateReader(int serialNo, int length, DeviceCode code) => HexBytes(new ReadText
    {
        Fixed = FixedPart.FixedHead,
        Timer = FixedPart.DataTimer,
        Length = FixedPart.DataLength,
        Command = FixedPart.ReadCommand,
        SubCommand = FixedPart.MultiPoint,
        DeviceCode = code.GetDescription(),
        StartPoint = _basicFunction.ConvertHEX(serialNo, WordLength.Three),
        Quantity = _basicFunction.ConvertHEX(length, WordLength.Two)
    });
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