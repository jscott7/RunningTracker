using System;
using System.Xml;
using System.Xml.Serialization;

namespace SportTracksXmlReader
{
    [XmlRoot(Namespace = "urn:uuid:D0EB2ED5-49B6-44e3-B13C-CF15BE7DD7DD")]
    public class Logbook
    {
        public Metadata Metadata;

        public Athlete Athlete;

        [XmlAttribute(AttributeName = "version")]
        public string Version;
       
        [XmlAttribute(AttributeName = "referenceId")]
        public Guid ReferenceId;

        [XmlArrayItem("Activity")]
        public Activity[] Activities;
    
        public EquipmentItem[] Equipment;
       
        [XmlArrayItem("Category")]
        public Category[] ClimbZones;

        [XmlArrayItem("Category")]
        public Category[] SpeedZones;

        [XmlArrayItem("Category")]
        public Category[] HeartRateZones;

        [XmlArrayItem("Category")]
        public Category[] CadenceZones;

        [XmlArrayItem("Category")]
        public Category[] PowerZones;

        [XmlArrayItem("Field")]
        public CustomDataFieldDefinition[] CustomDataFieldDefinitions;

        [XmlArrayItem("Category")]
        public ActivityCategory[] ActivityCategories;
    }
}
