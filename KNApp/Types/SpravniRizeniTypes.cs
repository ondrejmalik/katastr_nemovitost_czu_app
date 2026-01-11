using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace KNApp.Types;

public class SpravniRizeniData
{
    [JsonPropertyName("operace")]
    public required List<Operace> Operace { get; set; }

    [JsonPropertyName("predmet")]
    public required List<Predmet> Predmet { get; set; }

    [JsonPropertyName("ucastnici")]
    public required List<Ucastnik> Ucastnici { get; set; }

    [SetsRequiredMembers]
    public SpravniRizeniData()
    {
        Operace = new List<Operace>();
        Predmet = new List<Predmet>();
        Ucastnici = new List<Ucastnik>();
    }
}

public class Operace
{
    [JsonPropertyName("operace_datum")]
    public required string OperaceDatum { get; set; }

    [JsonPropertyName("operace_popis")]
    public required string OperacePopis { get; set; }

    [SetsRequiredMembers]
    public Operace() { OperaceDatum = string.Empty; OperacePopis = string.Empty; }
}

public class Predmet
{
    [JsonPropertyName("poznamka")]
    public required string Poznamka { get; set; }

    [JsonPropertyName("predmet")]
    public required string Nazev { get; set; }

    [SetsRequiredMembers]
    public Predmet() { Poznamka = string.Empty; Nazev = string.Empty; }
}

public class Ucastnik
{
    [JsonPropertyName("typ_ucastnika")]
    public required string TypUcastnika { get; set; }

    [JsonPropertyName("ucastnik_jmeno")]
    public required string UcastnikJmeno { get; set; }

    [SetsRequiredMembers]
    public Ucastnik() { TypUcastnika = string.Empty; UcastnikJmeno = string.Empty; }
}