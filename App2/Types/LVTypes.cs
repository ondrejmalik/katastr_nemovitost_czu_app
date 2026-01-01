using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace App2.Types;

public class LVData
{
    [JsonPropertyName("part_a")] public List<PartA>? PartA { get; set; }

    [JsonPropertyName("part_b")] public List<PartB>? PartB { get; set; }

    [JsonPropertyName("part_b_majitel")] public List<PartBMajitel>? PartBMajitel { get; set; }

    [JsonPropertyName("part_b_parcela")] public List<PartBParcela>? PartBParcela { get; set; }

    [JsonPropertyName("part_c")] public List<PartC>? PartC { get; set; }

    [JsonPropertyName("part_d")] public List<PartD>? PartD { get; set; }

    [JsonPropertyName("part_f")] public List<PartF>? PartF { get; set; }
}

public class PartA
{
    [JsonPropertyName("bydliste")] public string? Bydliste { get; set; }

    [JsonPropertyName("jmeno")] public string? Jmeno { get; set; }

    [JsonPropertyName("podil_setin")] public int PodilSetin { get; set; }

    [JsonPropertyName("prijmeni")] public string? Prijmeni { get; set; }
}

public class PartB
{
    [JsonPropertyName("cislo_popisne")] public string? CisloPopisne { get; set; }

    [JsonPropertyName("je_stavebni")] public bool JeStavebni { get; set; }

    [JsonPropertyName("nazev_ku")] public string? NazevKu { get; set; }

    [JsonPropertyName("parcelni_cislo")] public int ParcelniCislo { get; set; }

    [JsonPropertyName("ulice")] public string? Ulice { get; set; }
}

public class PartBMajitel
{
    [JsonPropertyName("cast_parcely_opravnena")]
    public int CastParcelyOpravnena { get; set; }

    [JsonPropertyName("datum_pravnich_ucinku")]
    public string? DatumPravnichUcinku { get; set; }

    [JsonPropertyName("datum_zrizeni")] public string? DatumZrizeni { get; set; }

    [JsonPropertyName("ico_povinny")] public string? IcoPovinny { get; set; }

    [JsonPropertyName("je_stavebni_opravnena")]
    public bool JeStavebniOpravnena { get; set; }

    [JsonPropertyName("jmeno_povinny")] public string? JmenoPovinny { get; set; }

    [JsonPropertyName("parcelni_cislo_opravnena")]
    public int ParcelniCisloOpravnena { get; set; }

    [JsonPropertyName("popis")] public string? Popis { get; set; }

    [JsonPropertyName("prijmeni_povinny")] public string? PrijmeniPovinny { get; set; }

    [JsonPropertyName("rodne_cislo_povinny")]
    public string? RodneCisloPovinny { get; set; }

    [JsonPropertyName("titul_povinny")] public string? TitulPovinny { get; set; }
}

public class PartBParcela
{
    [JsonPropertyName("cast_parcely_opravnena")]
    public int CastParcelyOpravnena { get; set; }

    [JsonPropertyName("cast_parcely_povinna")]
    public int CastParcelyPovinna { get; set; }

    [JsonPropertyName("datum_pravnich_ucinku")]
    public string? DatumPravnichUcinku { get; set; }

    [JsonPropertyName("datum_zrizeni")] public string? DatumZrizeni { get; set; }

    [JsonPropertyName("je_stavebni_opravnena")]
    public bool JeStavebniOpravnena { get; set; }

    [JsonPropertyName("je_stavebni_povinna")]
    public bool JeStavebniPovinna { get; set; }

    [JsonPropertyName("parcelni_cislo_opravnena")]
    public int ParcelniCisloOpravnena { get; set; }

    [JsonPropertyName("parcelni_cislo_povinna")]
    public int ParcelniCisloPovinna { get; set; }

    [JsonPropertyName("popis")] public string? Popis { get; set; }
}

public class PartC
{
    [JsonPropertyName("cast_parcely_opravnena")]
    public int CastParcelyOpravnena { get; set; }

    [JsonPropertyName("cast_parcely_povinna")]
    public int CastParcelyPovinna { get; set; }

    [JsonPropertyName("datum_pravnich_ucinku")]
    public string? DatumPravnichUcinku { get; set; }

    [JsonPropertyName("datum_zrizeni")] public string? DatumZrizeni { get; set; }

    [JsonPropertyName("je_stavebni_opravnena")]
    public bool JeStavebniOpravnena { get; set; }

    [JsonPropertyName("je_stavebni_povinna")]
    public bool JeStavebniPovinna { get; set; }

    [JsonPropertyName("parcelni_cislo_opravnena")]
    public int ParcelniCisloOpravnena { get; set; }

    [JsonPropertyName("parcelni_cislo_povinna")]
    public int ParcelniCisloPovinna { get; set; }

    [JsonPropertyName("popis")] public string? Popis { get; set; }
}

public class PartD
{
    [JsonPropertyName("cast_parcely")] public int CastParcely { get; set; }

    [JsonPropertyName("cislo_rizeni")] public int CisloRizeni { get; set; }

    [JsonPropertyName("je_stavebni")] public bool JeStavebni { get; set; }

    [JsonPropertyName("nazev_katastralniho_uzemi")]
    public string? NazevKatastralnihoUzemi { get; set; }

    [JsonPropertyName("parcelni_cislo")] public int ParcelniCislo { get; set; }

    [JsonPropertyName("rok_rizeni")] public int RokRizeni { get; set; }

    [JsonPropertyName("typ_rizeni_zkratka")]
    public string? TypRizeniZkratka { get; set; }
}

public class PartF
{
    [JsonPropertyName("cast_parcely")] public int CastParcely { get; set; }

    [JsonPropertyName("hodnota")] public int Hodnota { get; set; }

    [JsonPropertyName("je_stavebni")] public bool JeStavebni { get; set; }

    [JsonPropertyName("parcelni_cislo")] public int ParcelniCislo { get; set; }
}