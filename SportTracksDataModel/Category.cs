using System;
using System.Xml.Serialization;

namespace SportTracksXmlReader
{
    public class Category
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name;
 
        [XmlAttribute(AttributeName = "referenceId")]
        public Guid ReferenceId;

        [XmlArrayItem("Zone")]
        public Zone[] Zones;
    }
}
