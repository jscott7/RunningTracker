using System;
using System.Xml.Serialization;

namespace SportTracksXmlReader
{
    public class Lap
    {
        [XmlAttribute(AttributeName = "startTime")]
        public DateTime StartTime;

        [XmlAttribute(AttributeName = "totalTime")]
        public float TotalTime;
      
        [XmlAttribute(AttributeName = "totalDistance")]
        public float TotalDistance;

        [XmlAttribute(AttributeName = "elevationChange")]
        public float ElevationChange;

        [XmlAttribute(AttributeName = "avgCadence")]
        public UInt16 AvgCadence;

        [XmlAttribute(AttributeName = "totalCalories")]
        public UInt16 TotalCalories;
    }
}
