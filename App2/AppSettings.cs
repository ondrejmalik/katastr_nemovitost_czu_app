using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using App2;

public static class AppSettings
{
    private class SettingsData
    {
        public string Address { get; set; }
        public int Port { get; set; }
    }

    private static string _address = "127.0.0.1";
    private static int _port = 3000;

    private static Uri? _baseAddress
    {
        get
        {
            try
            {
                return new Uri($"http://{_address}:{_port}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default;
            }
        }
    }

    static AppSettings()
    {
        Load();
    }

    public static string Address
    {
        get => _address;
        set
        {
            _address = value;
            HttpService.SetBaseAddress(_baseAddress);
            _ = Save();
        }
    }

    public static int Port
    {
        get => _port;
        set
        {
            _port = value;
            HttpService.SetBaseAddress(_baseAddress);
            _ = Save();
        }
    }

    public static Uri BaseAddress => _baseAddress;

    public static string AuthPassword { get; set; }

    public static async Task Save()
    {
        var settings = new SettingsData()
        {
            Address = Address,
            Port = Port,
        };
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string katastrPath = Path.Combine(appDataPath, "katastr");
        Directory.CreateDirectory(katastrPath);
        string filePath = Path.Combine(katastrPath, "settings.json");
        string json = JsonSerializer.Serialize(settings);
        await File.WriteAllTextAsync(filePath, json);
    }

    public static void Load()
    {
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string katastrPath = Path.Combine(appDataPath, "katastr");

        string filePath = Path.Combine(katastrPath, "settings.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            var settings = JsonSerializer.Deserialize<SettingsData>(json);
            Address = settings!.Address;
            Port = settings.Port;
        }
    }
}