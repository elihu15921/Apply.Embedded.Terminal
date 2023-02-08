namespace IIoT.Infrastructure.Utilities;
public static class NeutralUtility
{
    public static string UseEncryptAES(this string text)
    {
        using var aes = Aes.Create();
        using var encryptor = aes.CreateEncryptor(Encoding.UTF8.GetBytes(nameof(IIoT).ToMd5()), aes.IV);
        {
            using MemoryStream msEncrypt = new();
            using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (StreamWriter swEncrypt = new(csEncrypt)) swEncrypt.Write(text);
            {
                var iv = aes.IV;
                var decryptedContent = msEncrypt.ToArray();
                var result = new byte[iv.Length + decryptedContent.Length];
                Buffer.BlockCopy(iv, default, result, default, iv.Length);
                Buffer.BlockCopy(decryptedContent, default, result, iv.Length, decryptedContent.Length);
                return Convert.ToBase64String(result);
            }
        }
    }
    public static string UseDecryptAES(this string text)
    {
        var iv = new byte[16];
        var fullCipher = Convert.FromBase64String(text);
        var cipher = new byte[fullCipher.Length - iv.Length];
        Buffer.BlockCopy(fullCipher, default, iv, default, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipher, default, fullCipher.Length - iv.Length);
        {
            using var aes = Aes.Create();
            using var decryptor = aes.CreateDecryptor(Encoding.UTF8.GetBytes(nameof(IIoT).ToMd5()), iv);
            using MemoryStream msDecrypt = new(cipher);
            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new(csDecrypt);
            return srDecrypt.ReadToEnd();
        }
    }
    public static T RefreshFile<T>(this T entity)
    {
        YamlSource source = new()
        {
            Path = Menu.ProfilePath,
            FileProvider = null,
            Optional = default,
            ReloadOnChange = default
        };
        source.ResolveFileProvider();
        ConfigurationBuilder builder = new();
        builder.Add(source);
        builder.Build().Bind(entity);
        return entity;
    }
    public static async ValueTask CreateFileAaync<T>(this T entity, bool cover = default)
    {
        if ((!File.Exists(Menu.ProfilePath) || cover) && entity is not null)
        {
            await File.WriteAllTextAsync(Menu.ProfilePath, new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build().Serialize(entity), Encoding.UTF8);
        }
    }
    public static void UseTriggers(this IApplicationBuilder app) => Array.ForEach(app.ApplicationServices.GetRequiredService<IEntranceTrigger[]>(), item => Task.Run(() => item.BuildAsync()));
    public static string Joint(this string front, string latter = "", string tag = ".") => $"{front}{tag}{latter}";
    public static string GetRootNamespace(this Assembly assembly) => assembly.GetName().Name!.Replace("FFG".Joint(), string.Empty);
    public static string GetDescription(this Type type, string name) => type.GetRuntimeField(name)!.GetCustomAttribute<DescriptionAttribute>()!.Description;
    public static string GetDescription(this Enum @enum) => @enum.GetType().GetRuntimeField(@enum.ToString())!.GetCustomAttribute<DescriptionAttribute>()!.Description;
    public static IDictionary<string, (int number, string description)> GetDescription<T>()
    {
        Dictionary<string, (int number, string description)> results = new();
        foreach (Enum @enum in Enum.GetValues(typeof(T))) results.Add(@enum.ToString(), (@enum.GetHashCode(), @enum.GetDescription()));
        return results.ToImmutableDictionary();
    }
    public static T? ToObject<T>(this string content) => JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
    {
        AllowTrailingCommas = true,
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    });
    public static T? ToObject<T>(this byte[] contents) => JsonSerializer.Deserialize<T>(contents, new JsonSerializerOptions
    {
        AllowTrailingCommas = true,
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    });
    public static string ToJson<T>(this T @object, bool indented = false) => JsonSerializer.Serialize(@object, typeof(T), new JsonSerializerOptions
    {
        WriteIndented = indented,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    });
}