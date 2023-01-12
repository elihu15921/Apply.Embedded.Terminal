namespace IIoT.Domain.Functions;
internal sealed class BasicFunction : IBasicFunction
{
    public async ValueTask InitialProfile()
    {
        try
        {
            MainProfile profile = new();
            await profile.CreateFileAaync();
            Profile = profile.RefreshFile();
        }
        catch (Exception e)
        {
            Log.Fatal(HistoryFoot.Title, nameof(BasicFunction).Joint(nameof(InitialProfile)), new
            {
                e.Message,
                e.StackTrace
            });
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
    public ArrayPool<byte> BytePool { get; } = ArrayPool<byte>.Shared;
}