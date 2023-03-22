using System;
using System.Xml.Serialization;

namespace SportTracksXmlReader
{
    public class DistanceMarker
    {
        [XmlAttribute(AttributeName = "distance")]
        public float Distance;
    }
}
