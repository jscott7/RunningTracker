using System;
using System.Xml.Serialization;

namespace SportTracksXmlReader
{
    public class EquipmentActivity
    {
        [XmlAttribute(AttributeName = "referenceId")]
        public Guid ReferenceId;
    }
}
