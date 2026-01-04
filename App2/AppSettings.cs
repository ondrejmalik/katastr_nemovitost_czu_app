using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using App2;

public static class AppSettings
{
    // Changed to public to ensure serialization works in Release builds (Trimming/AOT safe)
    public class SettingsData
    {
        public string Address { get; set; }
        public int Port { get; set; }
    }

    private static string _address = "127.0.0.1";
    private static int _port = 3000;
    
    // Flag to prevent Save() from triggering during Load()
    private static bool _isLoading = false;
    
    // Semaphore to prevent concurrent file writes
    private static readonly SemaphoreSlim _saveLock = new(1, 1);

    private static Uri? _baseAddress
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_address))
            {
                return null;
            }

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
            // Only save if we are not currently loading settings
            if (!_isLoading)
            {
                _ = Save();
            }
        }
    }

    public static int Port
    {
        get => _port;
        set
        {
            _port = value;
            HttpService.SetBaseAddress(_baseAddress);
            // Only save if we are not currently loading settings
            if (!_isLoading)
            {
                _ = Save();
            }
        }
    }

    public static Uri BaseAddress => _baseAddress;

    public static string AuthPassword { get; set; }

    public static async Task Save()
    {
        // Use Semaphore to ensure only one write happens at a time
        await _saveLock.WaitAsync();
        try
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
        catch (Exception ex)
        {
            // Log exception to prevent crash on background thread
            Console.WriteLine($"Error saving settings: {ex.Message}");
        }
        finally
        {
            _saveLock.Release();
        }
    }

    public static void Load()
    {
        _isLoading = true;
        try
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string katastrPath = Path.Combine(appDataPath, "katastr");

            string filePath = Path.Combine(katastrPath, "settings.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var settings = JsonSerializer.Deserialize<SettingsData>(json);
                if (settings != null)
                {
                    Address = settings.Address;
                    Port = settings.Port;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading settings: {ex.Message}");
        }
        finally
        {
            _isLoading = false;
        }
    }
}