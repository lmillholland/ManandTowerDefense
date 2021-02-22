using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class Wave
{
    [XmlArray("Battalions"), XmlArrayItem("Battalion")]
    public List<Battalion> battalions;
}