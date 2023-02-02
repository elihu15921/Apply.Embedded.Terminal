using static IIoT.Domain.Shared.Functions.Hosts.IMitsubishiHost;

namespace IIoT.Domain.Functions.Hosts;
internal sealed class MitsubishiHost : IMitsubishiHost
{
    readonly IBasicExpert _basic;
    readonly ITimeserieWrapper _timeserie;
    public MitsubishiHost(IBasicExpert basic, ITimeserieWrapper timeserie)
    {
        _basic = basic;
        _timeserie = timeserie;
    }
    public async ValueTask CreateAsync(IPAddress address)
    {
        await Warship.ConnectAsync(new IPEndPoint(address, Port));
        await PushInformationTrunkAsync(Reader(96, 1, DeviceCode.D));
        await PushMaintanenceItemAsync(Reader(9571, 29, DeviceCode.R));
        await PushSpindleLifeAsync(Reader(910, 15, DeviceCode.D), Reader(9200, 30, DeviceCode.R));
        {
            Warship.Shutdown(SocketShutdown.Both);
            Warship.Close();
            Warship.Dispose();
        }
    }
    async ValueTask PushInformationTrunkAsync(byte[] values)
    {
        await Warship.SendAsync(values);
        var buffers = _basic.BytePool.Rent(16);
        var receives = Capture(BitConverter.ToString(buffers, default, await Warship.ReceiveAsync(buffers))).ToArray();
        await _timeserie.BasicInformation.InsertAsync(new()
        {
            Status = receives[0],
            Identifier = string.Empty,
            Timestamp = DateTime.UtcNow
        });
    }
    async ValueTask PushMaintanenceItemAsync(byte[] values)
    {
        await Warship.SendAsync(values);
        var timestamp = DateTime.UtcNow;
        var buffers = _basic.BytePool.Rent(128);
        var receives = Capture(BitConverter.ToString(buffers, default, await Warship.ReceiveAsync(buffers))).ToArray();
        await _timeserie.MaintenanceWeekly.InsertAsync(new[]
        {
           new IMaintenanceWeekly.Entity
           {
               SerialNo = "1",
               CumulativeDay = receives[0],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceWeekly.Entity
           {
               SerialNo = "2",
               CumulativeDay = receives[1],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceWeekly.Entity
           {
               SerialNo = "3",
               CumulativeDay = receives[2],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceWeekly.Entity
           {
               SerialNo = "4",
               CumulativeDay = receives[3],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceWeekly.Entity
           {
               SerialNo = "5",
               CumulativeDay = receives[4],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceWeekly.Entity
           {
               SerialNo = "6",
               CumulativeDay = receives[5],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceWeekly.Entity
           {
               SerialNo = "7",
               CumulativeDay = receives[6],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceWeekly.Entity
           {
               SerialNo = "8",
               CumulativeDay = receives[7],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceWeekly.Entity
           {
               SerialNo = "9",
               CumulativeDay = receives[8],
               Identifier= string.Empty,
               Timestamp = timestamp
           }
        });
        await _timeserie.MaintenanceMonthly.InsertAsync(new[]
        {
           new IMaintenanceMonthly.Entity
           {
               SerialNo = "1",
               CumulativeDay = receives[9],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceMonthly.Entity
           {
               SerialNo = "2",
               CumulativeDay = receives[10],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceMonthly.Entity
           {
               SerialNo = "3",
               CumulativeDay = receives[11],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceMonthly.Entity
           {
               SerialNo = "4",
               CumulativeDay = receives[12],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceMonthly.Entity
           {
               SerialNo = "5",
               CumulativeDay = receives[13],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceMonthly.Entity
           {
               SerialNo = "6",
               CumulativeDay = receives[14],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceMonthly.Entity
           {
               SerialNo = "7",
               CumulativeDay = receives[15],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceMonthly.Entity
           {
               SerialNo = "8",
               CumulativeDay = receives[16],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceMonthly.Entity
           {
               SerialNo = "9",
               CumulativeDay = receives[17],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceMonthly.Entity
           {
               SerialNo = "10",
               CumulativeDay = receives[18],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceMonthly.Entity
           {
               SerialNo = "11",
               CumulativeDay = receives[19],
               Identifier= string.Empty,
               Timestamp = timestamp
           }
        });
        await _timeserie.MaintenanceYear.InsertAsync(new[]
        {
           new IMaintenanceYear.Entity
           {
               SerialNo = "1",
               CumulativeDay = receives[20],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceYear.Entity
           {
               SerialNo = "2",
               CumulativeDay = receives[21],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceYear.Entity
           {
               SerialNo = "3",
               CumulativeDay = receives[22],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceYear.Entity
           {
               SerialNo = "4",
               CumulativeDay = receives[23],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceYear.Entity
           {
               SerialNo = "5",
               CumulativeDay = receives[24],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceYear.Entity
           {
               SerialNo = "6",
               CumulativeDay = receives[25],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceYear.Entity
           {
               SerialNo = "7",
               CumulativeDay = receives[26],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceYear.Entity
           {
               SerialNo = "8",
               CumulativeDay = receives[27],
               Identifier= string.Empty,
               Timestamp = timestamp
           },
           new IMaintenanceYear.Entity
           {
               SerialNo = "9",
               CumulativeDay = receives[28],
               Identifier= string.Empty,
               Timestamp = timestamp
           }
        });
        _basic.BytePool.Return(buffers);
    }
    async ValueTask PushSpindleLifeAsync(byte[] flashes, byte[] keeps)
    {
        await Warship.SendAsync(flashes);
        var flasheBuffers = _basic.BytePool.Rent(64);
        var flasheReceives = Capture(BitConverter.ToString(flasheBuffers, default, await Warship.ReceiveAsync(flasheBuffers))).ToArray();
        _basic.BytePool.Return(flasheBuffers);
        await Warship.SendAsync(keeps);
        var keepBuffers = _basic.BytePool.Rent(128);
        var keepReceives = Capture(BitConverter.ToString(keepBuffers, default, await Warship.ReceiveAsync(keepBuffers))).ToArray();
        _basic.BytePool.Return(keepBuffers);
        await _timeserie.LifespanSpeed.InsertAsync(new[]
        {
            new ILifespanSpeed.Entity
            {
                Range = nameof(ILifespanSpeed.Entity.SpeedRange.A),
                Hour = keepReceives[1],
                Minute = keepReceives[0],
                Second = flasheReceives[0],
                Identifier = string.Empty,
                Timestamp = DateTime.UtcNow
            },
            new ILifespanSpeed.Entity
            {
                Range = nameof(ILifespanSpeed.Entity.SpeedRange.B),
                Hour = keepReceives[3],
                Minute = keepReceives[2],
                Second = flasheReceives[1],
                Identifier = string.Empty,
                Timestamp = DateTime.UtcNow
            },
            new ILifespanSpeed.Entity
            {
                Range = nameof(ILifespanSpeed.Entity.SpeedRange.C),
                Hour = keepReceives[5],
                Minute = keepReceives[4],
                Second = flasheReceives[2],
                Identifier = string.Empty,
                Timestamp = DateTime.UtcNow
            },
            new ILifespanSpeed.Entity
            {
                Range = nameof(ILifespanSpeed.Entity.SpeedRange.D),
                Hour = keepReceives[7],
                Minute = keepReceives[6],
                Second = flasheReceives[3],
                Identifier = string.Empty,
                Timestamp = DateTime.UtcNow
            },
            new ILifespanSpeed.Entity
            {
                Range = nameof(ILifespanSpeed.Entity.SpeedRange.E),
                Hour = keepReceives[9],
                Minute = keepReceives[8],
                Second = flasheReceives[4],
                Identifier = string.Empty,
                Timestamp = DateTime.UtcNow
            },
            new ILifespanSpeed.Entity
            {
                Range = nameof(ILifespanSpeed.Entity.SpeedRange.F),
                Hour = keepReceives[11],
                Minute = keepReceives[10],
                Second = flasheReceives[5],
                Identifier = string.Empty,
                Timestamp = DateTime.UtcNow
            },
            new ILifespanSpeed.Entity
            {
                Range = nameof(ILifespanSpeed.Entity.SpeedRange.G),
                Hour = keepReceives[13],
                Minute = keepReceives[12],
                Second = flasheReceives[6],
                Identifier = string.Empty,
                Timestamp = DateTime.UtcNow
            },
            new ILifespanSpeed.Entity
            {
                Range = nameof(ILifespanSpeed.Entity.SpeedRange.H),
                Hour = keepReceives[15],
                Minute = keepReceives[14],
                Second = flasheReceives[7],
                Identifier = string.Empty,
                Timestamp = DateTime.UtcNow
            },
            new ILifespanSpeed.Entity
            {
                Range = nameof(ILifespanSpeed.Entity.SpeedRange.I),
                Hour = keepReceives[17],
                Minute = keepReceives[16],
                Second = flasheReceives[8],
                Identifier = string.Empty,
                Timestamp = DateTime.UtcNow
            },
            new ILifespanSpeed.Entity
            {
                Range = nameof(ILifespanSpeed.Entity.SpeedRange.J),
                Hour = keepReceives[19],
                Minute = keepReceives[18],
                Second = flasheReceives[9],
                Identifier = string.Empty,
                Timestamp = DateTime.UtcNow
            },
            new ILifespanSpeed.Entity
            {
                Range = nameof(ILifespanSpeed.Entity.SpeedRange.K),
                Hour = keepReceives[21],
                Minute = keepReceives[20],
                Second = flasheReceives[10],
                Identifier = string.Empty,
                Timestamp = DateTime.UtcNow
            },
            new ILifespanSpeed.Entity
            {
                Range = nameof(ILifespanSpeed.Entity.SpeedRange.L),
                Hour = keepReceives[23],
                Minute = keepReceives[22],
                Second = flasheReceives[11],
                Identifier = string.Empty,
                Timestamp = DateTime.UtcNow
            },
            new ILifespanSpeed.Entity
            {
                Range = nameof(ILifespanSpeed.Entity.SpeedRange.M),
                Hour = keepReceives[25],
                Minute = keepReceives[24],
                Second = flasheReceives[12],
                Identifier = string.Empty,
                Timestamp = DateTime.UtcNow
            },
            new ILifespanSpeed.Entity
            {
                Range = nameof(ILifespanSpeed.Entity.SpeedRange.N),
                Hour = keepReceives[27],
                Minute = keepReceives[26],
                Second = flasheReceives[13],
                Identifier = string.Empty,
                Timestamp = DateTime.UtcNow
            },
            new ILifespanSpeed.Entity
            {
                Range = nameof(ILifespanSpeed.Entity.SpeedRange.O),
                Hour = keepReceives[29],
                Minute = keepReceives[28],
                Second = flasheReceives[14],
                Identifier = string.Empty,
                Timestamp = DateTime.UtcNow
            }
        });
    }
    byte[] Reader(int serialNo, int length, DeviceCode code) => HexBytes(new ReadText
    {
        Fixed = FixedPart.FixedHead,
        Timer = FixedPart.DataTimer,
        Length = FixedPart.DataLength,
        Command = FixedPart.ReadCommand,
        SubCommand = FixedPart.MultiPoint,
        DeviceCode = code.GetDescription(),
        StartPoint = _basic.ConvertHEX(serialNo, WordLength.Three),
        Quantity = _basic.ConvertHEX(length, WordLength.Two)
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