using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace KNApp;

/// <summary>
/// tohle musi byt service protoze se zapina 2 vteriny
/// </summary>
public static class HttpService
{
    private static HttpClient _httpClient;

    static HttpService()
    {
        var handler = new SocketsHttpHandler
        {
            UseProxy = false,

            PooledConnectionLifetime = TimeSpan.FromMinutes(5)
        };

        _httpClient = new HttpClient(handler)
        {
            BaseAddress = AppSettings.BaseAddress
        };
    }

    public static void SetBaseAddress(Uri uri)
    {
        var handler = new SocketsHttpHandler
        {
            UseProxy = false,

            PooledConnectionLifetime = TimeSpan.FromMinutes(5)
        };
        _httpClient = new HttpClient(handler)
        {
            BaseAddress = AppSettings.BaseAddress
        };
    }

    public static async Task<HttpResponseMessage> GetData(string url)
    {
        return await _httpClient.GetAsync(url);
    }

    private static void AddAuthCookie(HttpRequestMessage request)
    {
        if (!string.IsNullOrEmpty(AppSettings.AuthPassword))
        {
            request.Headers.Add("Cookie", $"katastr_pw={AppSettings.AuthPassword}");
        }
    }

    public static async Task<HttpResponseMessage> DeleteData(string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        AddAuthCookie(request);
        return await _httpClient.SendAsync(request);
    }

    public static async Task<HttpResponseMessage> PutData(string url, HttpContent content)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, url);
        request.Content = content;
        AddAuthCookie(request);
        return await _httpClient.SendAsync(request);
    }

    public static async Task<HttpResponseMessage> PostData(string url, HttpContent content)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = content;
        AddAuthCookie(request);
        return await _httpClient.SendAsync(request);
    }
}