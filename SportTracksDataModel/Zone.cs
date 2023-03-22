using System.Xml.Serialization;

namespace SportTracksXmlReader
{
    public class Zone
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name;
     
        [XmlAttribute(AttributeName = "flat")]
        public bool Flat;
        
        [XmlAttribute(AttributeName = "low")]
        public float Low;

        [XmlAttribute(AttributeName = "high")]
        public float High;
    }
}
