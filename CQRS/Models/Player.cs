using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace CQRS.Models;

public partial class Player
{
    [System.Text.Json.Serialization.JsonIgnore]
    public int Id { get; set; }

    public int? ShirtNo { get; set; }

    public string? Name { get; set; }

    public int? Appearances { get; set; }

    public int? Goals { get; set; }
}
public partial class PostPlayer
{
    [JsonProperty("shirtNo")]
    [XmlElement("shirtNo")]
    public int? ShirtNo { get; set; }

    [JsonProperty("name")]
    [XmlElement("name")]
    public string? Name { get; set; }

    [JsonProperty("appearances")]
    [XmlElement("appearances")]
    public int? Appearances { get; set; }

    [JsonProperty("goals")]
    [XmlElement("goals")]
    public int? Goals { get; set; }
}
