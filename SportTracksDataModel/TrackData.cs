using System;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SportTracksXmlReader
{
    public class TrackData
    {
        [XmlAttribute(AttributeName = "version")]
        public UInt16 Version;

        [XmlText]
        public XmlNode[] ContentAsCData
        {
            get => new[] { new XmlDocument().CreateCDataSection(Data) };
            set {
                Data = value?[0].InnerText;
            }
        }

        [XmlIgnore]
        public string Data;
    }
}
