using System.Xml;
using System.Xml.Serialization;
 
[XmlRoot("Battalion")]
public class Battalion
{
    public string EnemyType;
    public int NumEnemies;
    public int Level;
}