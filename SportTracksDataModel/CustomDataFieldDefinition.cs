using System;
using System.Xml.Serialization;

namespace SportTracksXmlReader
{
    public class CustomDataFieldDefinition
    {
        [XmlAttribute(AttributeName = "id")]
        public string ID;

        [XmlAttribute(AttributeName = "objectType")]
        public string IActivity;

        [XmlAttribute(AttributeName = "dataType")]
        public Guid DataType;

        [XmlAttribute(AttributeName = "name")]
        public string Name;

        [XmlAttribute(AttributeName = "groupAgg")]
        public string GroupAgg;

        [XmlAttribute(AttributeName = "options")]
        public string Options;

        [XmlAttribute(AttributeName = "createdBy")]
        public Guid CreatedBy;
    }
}
