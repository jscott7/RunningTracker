using System;
using System.Xml.Serialization;

namespace SportTracksXmlReader
{
    public class EquipmentItem
    {
        public Metadata Metadata;

        [XmlAttribute(AttributeName = "referenceId")]
        public Guid ReferenceId;
      
        [XmlAttribute(AttributeName = "model")]
        public string Model;

        [XmlAttribute(AttributeName = "brand")]
        public string Brand;

        [XmlAttribute(AttributeName = "datePurchased")]
        public DateTime DatePurchased;

        [XmlAttribute(AttributeName = "type")]
        public string Type;

        [XmlAttribute(AttributeName = "expectedLifeKilometers")]
        public int ExpectedLifeKilometers;

        [XmlAttribute(AttributeName = "purchaseLocation")]
        public string PurchaseLocation;

        // Some invalid values in source data - set to string
        [XmlAttribute(AttributeName = "purchasePrice")] 
        public string PurchasePrice;

        [XmlAttribute(AttributeName = "notes")]
        public string Notes;

        [XmlAttribute(AttributeName = "inUse")]
        public bool InUse;

        [XmlAttribute(AttributeName = "displayWithActivity")]
        public bool DisplayWithActivity;

        [XmlArrayItem("Activity")]
        public EquipmentActivity[] Activities;
    }
}
