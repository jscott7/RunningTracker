using System;
using System.Xml.Serialization;

namespace SportTracksXmlReader
{
    public class HistoryEntry
    {
        [XmlAttribute(AttributeName = "date")]
        public DateTime Date;

        [XmlAttribute(AttributeName = "weight")]
        public float Weight;

        [XmlAttribute(AttributeName = "bmi")]
        public float Bmi;

        [XmlAttribute(AttributeName = "mood")]
        public UInt16 Mood;

        [XmlAttribute(AttributeName = "sleepHours")]
        public UInt16 SleepHours;
     
        [XmlAttribute(AttributeName = "sleepQuality")]
        public UInt16 SleepQuality;

        [XmlAttribute(AttributeName = "minHR")]
        public UInt16 MinHR;

        [XmlArrayItem("Value")]
        public CustomDataValue[] CustomData;
    }
}
