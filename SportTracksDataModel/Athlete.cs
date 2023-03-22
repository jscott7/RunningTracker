using System;
using System.Xml.Serialization;

namespace SportTracksXmlReader
{
    public class Athlete
    {
        public Metadata Metadata;

        [XmlAttribute(AttributeName = "referenceId")]
        public Guid ReferenceId;
     
        [XmlAttribute(AttributeName = "name")]
        public string Name;

        [XmlAttribute(AttributeName = "sex")]
        public string Sex;
       
        [XmlAttribute(AttributeName = "dateOfBirth")]
        public DateTime DateOfBirth;
       
        [XmlAttribute(AttributeName = "heightCentimeters")]
        public UInt16 HeightCentimeters;

        [XmlArrayItem("Entry")]
        public HistoryEntry[] History;
    }
}
