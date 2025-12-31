using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace App2;

/// <summary>
/// tohle musi byt service protoze se zapina 2 vteriny
/// </summary>
public static class HttpService
{
    private static readonly HttpClient HttpClient;

    static HttpService()
    {
        var handler = new SocketsHttpHandler
        {
            UseProxy = false,

            PooledConnectionLifetime = TimeSpan.FromMinutes(5)
        };

        HttpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("http://localhost:3000")
        };
    }

    public static async Task<HttpResponseMessage> GetData(string url)
    {
        return await HttpClient.GetAsync(url);
    }
}