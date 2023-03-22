using System;
using System.Xml.Serialization;

namespace SportTracksXmlReader
{
    public class Metadata
    {
        [XmlAttribute(AttributeName = "created")]
        public DateTime Created;

        [XmlAttribute(AttributeName = "modified")]
        public DateTime Modified;
    }
}
