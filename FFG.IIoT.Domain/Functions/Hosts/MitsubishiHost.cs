using static IIoT.Domain.Shared.Divisions.Turbos.ILifespanTurbo;
using static IIoT.Domain.Shared.Divisions.Turbos.IMaintenanceTurbo;
using static IIoT.Domain.Shared.Functions.Hosts.IMitsubishiHost;

namespace IIoT.Domain.Functions.Hosts;
internal sealed class MitsubishiHost : IMitsubishiHost
{
    readonly IMetaWrapper _meta;
    readonly IBasicExpert _basic;
    readonly ILifespanTurbo _lifespan;
    readonly IMaintenanceTurbo _maintenance;
    public MitsubishiHost(IMetaWrapper meta, IBasicExpert basic, ILifespanTurbo lifespan, IMaintenanceTurbo maintenance)
    {
        _meta = meta;
        _basic = basic;
        _lifespan = lifespan;
        _maintenance = maintenance;
    }
    public async ValueTask CreateAsync(IPAddress address)
    {
        await Warship.ConnectAsync(new IPEndPoint(address, Port));
        await PushMachineStatusAsync(Reader(96, 1, DeviceCode.D));
        await PushMaintanenceItemAsync(Reader(9571, 29, DeviceCode.R));
        await PushSpindleLifeAsync(Reader(910, 15, DeviceCode.D), Reader(9200, 30, DeviceCode.R));
        {
            Warship.Shutdown(SocketShutdown.Both);
            Warship.Close();
            Warship.Dispose();
        }
    }
    public Maintenance GetMaintenance() => new()
    {
        Weeklies = _maintenance.MitsubishiWeeklies,
        Monthlies = _maintenance.MitsubishiMonthlies,
        HalfYears = _maintenance.MitsubishiHalfYears
    };
    public SpindleLifespan GetSpindleLife() => new()
    {
        Speeds = _lifespan.MitsubishiSpindles
    };
    async ValueTask PushMachineStatusAsync(byte[] values)
    {
        await Warship.SendAsync(values);
        var buffers = _basic.BytePool.Rent(16);
        var receives = Capture(BitConverter.ToString(buffers, default, await Warship.ReceiveAsync(buffers))).ToArray();
        await _meta.Information.InsertAsync(new()
        {
            Status = receives[0],
            Timestamp = DateTime.UtcNow
        });
    }
    async ValueTask PushMaintanenceItemAsync(byte[] values)
    {
        await Warship.SendAsync(values);
        var buffers = _basic.BytePool.Rent(128);
        var receives = Capture(BitConverter.ToString(buffers, default, await Warship.ReceiveAsync(buffers))).ToArray();
        _maintenance.SetMitsubishiWeekly(new[]
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
        _maintenance.SetMitsubishiMonthly(new[]
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
        _maintenance.SetMitsubishiHalfYear(new[]
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
        _lifespan.SetMitsubishiSpindle(new[]
        {
            new MitsubishiSpindle
            {
                RangeNo = 1,
                Hour = keepReceives[1],
                Minute = keepReceives[0],
                Second = flasheReceives[0],
                Description = SpindleRange.A.GetDescription()
            },
            new MitsubishiSpindle
            {
                RangeNo = 2,
                Hour = keepReceives[3],
                Minute = keepReceives[2],
                Second = flasheReceives[1],
                Description = SpindleRange.B.GetDescription()
            },
            new MitsubishiSpindle
            {
                RangeNo = 3,
                Hour = keepReceives[5],
                Minute = keepReceives[4],
                Second = flasheReceives[2],
                Description = SpindleRange.C.GetDescription()
            },
            new MitsubishiSpindle
            {
                RangeNo = 4,
                Hour = keepReceives[7],
                Minute = keepReceives[6],
                Second = flasheReceives[3],
                Description = SpindleRange.D.GetDescription()
            },
            new MitsubishiSpindle
            {
                RangeNo = 5,
                Hour = keepReceives[9],
                Minute = keepReceives[8],
                Second = flasheReceives[4],
                Description = SpindleRange.E.GetDescription()
            },
            new MitsubishiSpindle
            {
                RangeNo = 6,
                Hour = keepReceives[11],
                Minute = keepReceives[10],
                Second = flasheReceives[5],
                Description = SpindleRange.F.GetDescription()
            },
            new MitsubishiSpindle
            {
                RangeNo = 7,
                Hour = keepReceives[13],
                Minute = keepReceives[12],
                Second = flasheReceives[6],
                Description = SpindleRange.G.GetDescription()
            },
            new MitsubishiSpindle
            {
                RangeNo = 8,
                Hour = keepReceives[15],
                Minute = keepReceives[14],
                Second = flasheReceives[7],
                Description = SpindleRange.H.GetDescription()
            },
            new MitsubishiSpindle
            {
                RangeNo = 9,
                Hour = keepReceives[17],
                Minute = keepReceives[16],
                Second = flasheReceives[8],
                Description = SpindleRange.I.GetDescription()
            },
            new MitsubishiSpindle
            {
                RangeNo = 10,
                Hour = keepReceives[19],
                Minute = keepReceives[18],
                Second = flasheReceives[9],
                Description = SpindleRange.J.GetDescription()
            },
            new MitsubishiSpindle
            {
                RangeNo = 11,
                Hour = keepReceives[21],
                Minute = keepReceives[20],
                Second = flasheReceives[10],
                Description = SpindleRange.K.GetDescription()
            },
            new MitsubishiSpindle
            {
                RangeNo = 12,
                Hour = keepReceives[23],
                Minute = keepReceives[22],
                Second = flasheReceives[11],
                Description = SpindleRange.L.GetDescription()
            },
            new MitsubishiSpindle
            {
                RangeNo = 13,
                Hour = keepReceives[25],
                Minute = keepReceives[24],
                Second = flasheReceives[12],
                Description = SpindleRange.M.GetDescription()
            },
            new MitsubishiSpindle
            {
                RangeNo = 14,
                Hour = keepReceives[27],
                Minute = keepReceives[26],
                Second = flasheReceives[13],
                Description = SpindleRange.N.GetDescription()
            },
            new MitsubishiSpindle
            {
                RangeNo = 15,
                Hour = keepReceives[29],
                Minute = keepReceives[28],
                Second = flasheReceives[14],
                Description = SpindleRange.O.GetDescription()
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