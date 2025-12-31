using System.Text.Json.Serialization;

namespace App2.Types;

public class MajitelData
{
    [JsonPropertyName("bydliste")]
    public string? Bydliste { get; set; }

    [JsonPropertyName("ico")]
    public string? Ico { get; set; }

    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("jmeno")]
    public string Jmeno { get; set; }

    [JsonPropertyName("prijmeni")]
    public string Prijmeni { get; set; }

    [JsonPropertyName("rodne_cislo")]
    public string? RodneCislo { get; set; }

    [JsonPropertyName("titul")]
    public string? Titul { get; set; }
}