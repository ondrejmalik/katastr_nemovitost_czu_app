using System.Text.Json.Serialization;

namespace KNApp.Types;

public class ParcelaData
{
    [JsonIgnore] public string KatastralniUzemi { get; set; }
    [JsonPropertyName("cast_parcely")] public int CastParcely { get; set; }

    [JsonPropertyName("cislo_lv")] public int CisloLv { get; set; }

    [JsonPropertyName("cislo_popisne")] public string? CisloPopisne { get; set; }

    [JsonPropertyName("hodnota")] public long Hodnota { get; set; }

    [JsonPropertyName("je_stavebni")] public bool JeStavebni { get; set; }

    [JsonPropertyName("parcelni_cislo")] public int ParcelniCislo { get; set; }

    [JsonPropertyName("ulice")] public string? Ulice { get; set; }

    [JsonPropertyName("vymera_metru_ctverecnich")]
    public string? VymeraMetruCtverecnich { get; set; }
}