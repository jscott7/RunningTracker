using System;
using System.Xml.Serialization;

namespace SportTracksXmlReader
{
    public class CustomDataValue
    {
        [XmlAttribute(AttributeName = "f")]
        public Guid F;
        
        [XmlAttribute(AttributeName = "v")]
        public UInt16 V;
    }
}
