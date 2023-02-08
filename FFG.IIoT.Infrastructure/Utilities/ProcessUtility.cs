﻿namespace IIoT.Infrastructure.Utilities;
public static class ProcessUtility
{
    public interface IEntranceTrigger
    {
        ValueTask BuildAsync();
    }
    public enum HostType
    {
        None = 0,
        Mitsubishi = 1,
        Fanuc = 2,
        Siemens = 3
    }
    public enum LanguageType
    {
        [Description("en-US")] English,
        [Description("zh-CN")] Simplified,
        [Description("zh-TW")] Traditional
    }
    public ref struct Label
    {
        public static string Username => "ffg";
        public static string Organize => "FFG-iMDS";
        public static string Passkey => "qEm6ssYVjmvgvOoPYS3EYJdbZRqrH/RgfSJ5M9/7gvA=";
    }
    public ref struct Local
    {
        public static int Broker => 1883;
        public static int Metadata => 8086;
        public static int Entrance => 17770;
        public static int Timeout => 30;
        public static int RetentionDay => 14;
        public static int DayToSeconds => 86400;
        public static TimeSpan RefreshTime => TimeSpan.FromSeconds(1);
        public static string Language { get; set; } = LanguageType.English.GetDescription();
    }
    public ref struct Menu
    {
        public static string Title => "[{0}] {1}";
        public static string DateFormat => "yyyy/MM/dd HH:mm:ss";
        public static string RootPath => Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? string.Empty;
        public static string HistoryPath => Path.Combine(RootPath, "..", "Logs");
        public static string ProfilePath => Path.Combine(RootPath, "..", $"Main.yml");

    }
}