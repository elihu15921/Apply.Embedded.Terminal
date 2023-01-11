namespace IIoT.Infrastructure.Sturdies;
public static class SturdyFoot
{
    public static string GetDescription(this Enum @enum) => @enum.GetType().GetRuntimeField(@enum.ToString())!.GetCustomAttribute<DescriptionAttribute>()!.Description;
    public static string GetDirectoryPath(this string fileName) => Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? string.Empty, fileName);
    public static string UseEncryptAES(this string text)
    {
        using var aes = Aes.Create();
        using var msEncrypt = new MemoryStream();
        using var encryptor = aes.CreateEncryptor(Encoding.UTF8.GetBytes(Passkey), aes.IV);
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        using (var swEncrypt = new StreamWriter(csEncrypt)) swEncrypt.Write(text);
        {
            var iv = aes.IV;
            var decryptedContent = msEncrypt.ToArray();
            var result = new byte[iv.Length + decryptedContent.Length];
            Buffer.BlockCopy(iv, default, result, default, iv.Length);
            Buffer.BlockCopy(decryptedContent, default, result, iv.Length, decryptedContent.Length);
            return Convert.ToBase64String(result);
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
            using var decryptor = aes.CreateDecryptor(Encoding.UTF8.GetBytes(Passkey), iv);
            using var msDecrypt = new MemoryStream(cipher);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            return srDecrypt.ReadToEnd();
        }
    }
    public static T RefreshFile<T>(this T entity)
    {
        YamlSource source = new()
        {
            Path = $"{Passkey}.yml".GetDirectoryPath(),
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
        var path = $"{Passkey}.yml".GetDirectoryPath();
        if ((!File.Exists(path) || cover) && entity is not null)
        {
            var yaml = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build().Serialize(entity);
            await File.WriteAllTextAsync(path, yaml, Encoding.UTF8);
        }
    }
    public static string Passkey => nameof(IIoT).ToMd5();
    public static TimeSpan RefreshTime => TimeSpan.FromSeconds(1);
}