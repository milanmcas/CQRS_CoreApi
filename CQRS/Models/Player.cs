using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace CQRS.Models;

public partial class Player
{
    [JsonIgnore]
    public int Id { get; set; }

    public int? ShirtNo { get; set; }

    public string? Name { get; set; }

    public int? Appearances { get; set; }

    public int? Goals { get; set; }
}
public partial class PostPlayer
{
    [XmlElement("shirtNo")]
    public int? ShirtNo { get; set; }
    [XmlElement("name")]
    public string? Name { get; set; }
    [XmlElement("appearances")]
    public int? Appearances { get; set; }
    [XmlElement("goals")]
    public int? Goals { get; set; }
}
