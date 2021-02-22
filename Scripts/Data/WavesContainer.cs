using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
 
[XmlRoot("Level")]
public class WavesContainer
{
    [XmlArray("Waves"), XmlArrayItem("Wave")]
    public List<Wave> waves = new List<Wave>();

    public static WavesContainer Load(string path)
    {
        /*
        Load XMl file into serializable objects.
        */
        var serializer = new XmlSerializer(typeof(WavesContainer));
        using(var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as WavesContainer;
        }
    }
}