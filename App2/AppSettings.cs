using System;

namespace App2;

public static class AppSettings
{
    private static Uri _baseAddress = new("http://127.0.0.1:3000");

    public static Uri BaseAddress
    {
        get => _baseAddress;
        set
        {
            _baseAddress = value;
            HttpService.SetBaseAddress(value);
        }
    }

    public static string AuthPassword { get; set; }
}