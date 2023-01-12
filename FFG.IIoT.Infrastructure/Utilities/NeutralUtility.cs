namespace IIoT.Infrastructure.Utilities;
public static class NeutralUtility
{
    public static string UseEncryptAES(this string text)
    {
        using var aes = Aes.Create();
        using var encryptor = aes.CreateEncryptor(Encoding.UTF8.GetBytes(Passkey), aes.IV);
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
            using var decryptor = aes.CreateDecryptor(Encoding.UTF8.GetBytes(Passkey), iv);
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
            Path = Path.Combine(RootDirectory, $"{Passkey}.yml"),
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
        var path = Path.Combine(RootDirectory, $"{Passkey}.yml");
        if ((!File.Exists(path) || cover) && entity is not null)
        {
            await File.WriteAllTextAsync(path, new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build().Serialize(entity), Encoding.UTF8);
        }
    }
    public static string Joint(this string front, string latter = "", string tag = ".") => $"{front}{tag}{latter}";
    public static string GetDescription(this Enum @enum) => @enum.GetType().GetRuntimeField(@enum.ToString())!.GetCustomAttribute<DescriptionAttribute>()!.Description;
    public static TimeSpan RefreshTime => TimeSpan.FromSeconds(1);
    public static string Passkey => nameof(IIoT).ToMd5();
    public static string RootDirectory => Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? string.Empty;
}