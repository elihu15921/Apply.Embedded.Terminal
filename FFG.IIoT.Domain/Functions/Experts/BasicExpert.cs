namespace IIoT.Domain.Functions.Experts;
internal sealed class BasicExpert : IBasicExpert
{
    public BasicExpert()
    {
        Transport = new MqttFactory().CreateMqttServer(new MqttServerOptionsBuilder().WithDefaultEndpoint().WithDefaultEndpointPort(Local.Broker).Build());
    }
    public async ValueTask InitialProfileAsync()
    {
        try
        {
            MainProfile profile = new();
            await profile.CreateFileAaync();
            Profile = profile.RefreshFile();
        }
        catch (Exception e)
        {
            Log.Fatal(Menu.Title, nameof(BasicExpert).Joint(nameof(InitialProfileAsync)), new
            {
                e.Message,
                e.StackTrace
            });
        }
    }
    public async ValueTask InitialPoolAsync(string url, string organize, string username, string password, string bucket)
    {
        using var result = new InfluxDBClient(url, username, password);
        var entity = await result.GetBucketsApi().FindBucketByNameAsync(bucket);
        if (entity is null)
        {
            var organizations = await result.GetOrganizationsApi().FindOrganizationsAsync(org: organize);
            BucketRetentionRules rule = new(BucketRetentionRules.TypeEnum.Expire, 100 * Local.DayToSeconds);
            await result.GetBucketsApi().CreateBucketAsync(bucket, rule, organizations[default].Id);
        }
    }
    public string ConvertHEX(in int quantity, in WordLength length)
    {
        var count = 1;
        string before = string.Empty, middle = string.Empty, after = string.Empty, temp = string.Empty;
        switch (length)
        {
            case WordLength.Two:
                {
                    var source = quantity.ToString("X4");
                    if (source.Length % 2 is not 0) source = source.PadRight(source.Length + (2 - source.Length % 2));
                    for (int upper = default; upper < source.Length; upper += 2)
                    {
                        for (int lower = default; lower < 2; lower++) temp += source[upper + lower];
                        if (count is 1) middle = temp;
                        else before = temp;
                        temp = string.Empty;
                        count++;
                    }
                    return $"{before}{middle}";
                }

            case WordLength.Three:
                {
                    var source = quantity.ToString("X6");
                    if (source.Length % 2 is not 0) source = source.PadRight(source.Length + (2 - source.Length % 2));
                    for (int upper = default; upper < source.Length; upper += 2)
                    {
                        for (int lower = default; lower < 2; lower++) temp += source[upper + lower];
                        if (count is 1) after = temp;
                        else if (count is 2) middle = temp;
                        else before = temp;
                        temp = string.Empty;
                        count++;
                    }
                    return $"{before}{middle}{after}";
                }
        }
        return string.Empty;
    }
    public MainProfile? Profile { get; set; }
    public required MqttServer Transport { get; init; }
    public ArrayPool<byte> BytePool { get; } = ArrayPool<byte>.Shared;
}