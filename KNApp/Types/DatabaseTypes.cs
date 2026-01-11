using System.Text.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace KNApp.Types;

public class KrajData
{
    [JsonPropertyName("id")] public long Id { get; set; }
    [JsonPropertyName("nazev")] public required string Nazev { get; set; }

    [SetsRequiredMembers]
    public KrajData() { Nazev = string.Empty; }
}

public class OkresData
{
    [JsonPropertyName("id")] public long Id { get; set; }
    [JsonPropertyName("kraj_id")] public long KrajId { get; set; }
    [JsonPropertyName("nazev")] public required string Nazev { get; set; }

    [SetsRequiredMembers]
    public OkresData() { Nazev = string.Empty; }
}

public class ObecData
{
    [JsonPropertyName("id")] public long Id { get; set; }                                           
    [JsonPropertyName("okres_id")] public long OkresId { get; set; }
    [JsonPropertyName("nazev")] public required string Nazev { get; set; }

    [SetsRequiredMembers]
    public ObecData() { Nazev = string.Empty; }
}

public class KatastralniUzemiData
{
    [JsonPropertyName("id")] public long Id { get; set; }
    [JsonPropertyName("obec_id")] public long ObecId { get; set; }
    [JsonPropertyName("nazev")] public required string Nazev { get; set; }

    [SetsRequiredMembers]
    public KatastralniUzemiData() { Nazev = string.Empty; }
}

public class BpejData
{
    [JsonPropertyName("id")] public long Id { get; set; }
    [JsonPropertyName("hodnota")] public long Hodnota { get; set; }
}

public class TypRizeniData
{
    [JsonPropertyName("id")] public long Id { get; set; }
    [JsonPropertyName("nazev")] public required string Nazev { get; set; }
    [JsonPropertyName("zkratka")] public required string Zkratka { get; set; }

    [SetsRequiredMembers]
    public TypRizeniData() { Nazev = string.Empty; Zkratka = string.Empty; }
}

public class TypOperaceData
{
    [JsonPropertyName("id")] public long Id { get; set; }
    [JsonPropertyName("popis")] public required string Popis { get; set; }

    [SetsRequiredMembers]
    public TypOperaceData() { Popis = string.Empty; }
}

public class TypUcastnikaData
{
    [JsonPropertyName("id")] public long Id { get; set; }
    [JsonPropertyName("nazev")] public required string Nazev { get; set; }

    [SetsRequiredMembers]
    public TypUcastnikaData() { Nazev = string.Empty; }
}

public class UcastnikRizeniData
{
    [JsonPropertyName("id")] public long Id { get; set; }
    [JsonPropertyName("jmeno")] public required string Jmeno { get; set; }

    [SetsRequiredMembers]
    public UcastnikRizeniData() { Jmeno = string.Empty; }
}

public class ListVlastnictviData
{
    [JsonPropertyName("id")] public long Id { get; set; }
    [JsonPropertyName("katastralni_uzemi_id")] public long KatastralniUzemiId { get; set; }
    [JsonPropertyName("cislo_lv")] public long CisloLv { get; set; }
    [JsonPropertyName("vlastnicky_hash")] public string? VlastnickyHash { get; set; }
}

public class ParcelaRowData
{
    [JsonPropertyName("id")] public long Id { get; set; }
    [JsonPropertyName("parcelni_cislo")] public long ParcelniCislo { get; set; }
    [JsonPropertyName("cast_parcely")] public int CastParcely { get; set; }
    [JsonPropertyName("je_stavebni")] public bool JeStavebni { get; set; }
    [JsonPropertyName("vymera_metru_ctverecnich")] public required string VymeraMetruCtverecnich { get; set; }
    [JsonPropertyName("ulice")] public string? Ulice { get; set; }
    [JsonPropertyName("cislo_popisne")] public string? CisloPopisne { get; set; }
    [JsonPropertyName("katastralni_uzemi_id")] public long KatastralniUzemiId { get; set; }
    [JsonPropertyName("bpej_id")] public long? BpejId { get; set; }
    [JsonPropertyName("list_vlastnictvi_id")] public long ListVlastnictviId { get; set; }

    [SetsRequiredMembers]
    public ParcelaRowData() { VymeraMetruCtverecnich = string.Empty; }
}

public class RizeniData
{
    [JsonPropertyName("id")] public long Id { get; set; }
    [JsonPropertyName("rok")] public int Rok { get; set; }
    [JsonPropertyName("cislo_rizeni")] public long CisloRizeni { get; set; }
    [JsonPropertyName("typ_rizeni_id")] public long TypRizeniId { get; set; }
    [JsonPropertyName("predmet")] public string? Predmet { get; set; }
    [JsonPropertyName("poznamka")] public string? Poznamka { get; set; }
}

public class VlastnictviData
{
    [JsonPropertyName("parcela_id")] public long ParcelaId { get; set; }
    [JsonPropertyName("majitel_id")] public long MajitelId { get; set; }
    [JsonPropertyName("podil_setin")] public long PodilSetin { get; set; }
}

public class BremenoParcelaParcelaData
{
    [JsonPropertyName("parcela_id")] public long ParcelaId { get; set; }
    [JsonPropertyName("parcela_povinna_id")] public long ParcelaPovinnaId { get; set; }
    [JsonPropertyName("popis")] public string? Popis { get; set; }
    [JsonPropertyName("datum_zrizeni")] public string? DatumZrizeni { get; set; }
    [JsonPropertyName("datum_pravnich_ucinku")] public string? DatumPravnichUcinku { get; set; }
}

public class BremenoParcelaMajitelData
{
    [JsonPropertyName("parcela_id")] public long ParcelaId { get; set; }
    [JsonPropertyName("majitel_povinny_id")] public long MajitelPovinnyId { get; set; }
    [JsonPropertyName("popis")] public string? Popis { get; set; }
    [JsonPropertyName("datum_zrizeni")] public string? DatumZrizeni { get; set; }
    [JsonPropertyName("datum_pravnich_ucinku")] public string? DatumPravnichUcinku { get; set; }
}

public class RizeniOperaceRowData
{
    [JsonPropertyName("rizeni_id")] public long RizeniId { get; set; }
    [JsonPropertyName("typ_operace_id")] public long TypOperaceId { get; set; }
    [JsonPropertyName("datum")] public string? Datum { get; set; }
}

public class PlombaData
{
    [JsonPropertyName("rizeni_id")] public long RizeniId { get; set; }
    [JsonPropertyName("parcela_id")] public long ParcelaId { get; set; }
}

public class UcastData
{
    [JsonPropertyName("rizeni_id")] public long RizeniId { get; set; }
    [JsonPropertyName("ucastnik_rizeni_id")] public long UcastnikRizeniId { get; set; }
    [JsonPropertyName("typ_ucastnika_id")] public long TypUcastnikaId { get; set; }
}
