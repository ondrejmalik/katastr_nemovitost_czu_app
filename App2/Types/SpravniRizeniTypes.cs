using System.Collections.Generic;

namespace App2.Types;

using System.Text.Json.Serialization;

public class SpravniRizeniData
{
    [JsonPropertyName("operace")]
    public List<Operace> Operace { get; set; }

    [JsonPropertyName("predmet")]
    public List<Predmet> Predmet { get; set; }

    [JsonPropertyName("ucastnici")]
    public List<Ucastnik> Ucastnici { get; set; }
}

public class Operace
{
    [JsonPropertyName("operace_datum")]
    public string OperaceDatum { get; set; }

    [JsonPropertyName("operace_popis")]
    public string OperacePopis { get; set; }
}

public class Predmet
{
    [JsonPropertyName("poznamka")]
    public string Poznamka { get; set; }

    [JsonPropertyName("predmet")]
    public string Nazev { get; set; }
}

public class Ucastnik
{
    [JsonPropertyName("typ_ucastnika")]
    public string TypUcastnika { get; set; }

    [JsonPropertyName("ucastnik_jmeno")]
    public string UcastnikJmeno { get; set; }
}