using System;
using System.Xml;
using System.Xml.Serialization;

namespace SportTracksXmlReader
{
    [XmlRoot(Namespace = "urn:uuid:D0EB2ED5-49B6-44e3-B13C-CF15BE7DD7DD") ]
    public class Activity
    {
        public Metadata Metadata;

        [XmlAttribute(AttributeName = "referenceId")]
        public Guid ReferenceId;

        [XmlAttribute(AttributeName = "startTime")]
        public DateTime StartTime;

        [XmlAttribute(AttributeName = "timeZoneUtcOffset")]
        public float TimeZoneUtcOffset;

        [XmlAttribute(AttributeName = "hasStartTime")]
        public bool HasStartTime;

        [XmlAttribute(AttributeName = "totalTime")]
        public float TotalTime;

        [XmlAttribute(AttributeName = "totalDistance")]
        public float TotalDistance;

        [XmlAttribute(AttributeName = "totalCalories")]
        public UInt16 TotalCalories;

        [XmlAttribute(AttributeName = "categoryName")]
        public string CategoryName;

        [XmlAttribute(AttributeName = "categoryId")]
        public Guid CategoryId;

        [XmlAttribute(AttributeName = "location")]
        public string location;

        [XmlAttribute(AttributeName = "useEnteredData")]
        public bool UseEnteredData;

        [XmlArrayItem("DistanceMarker")]
        public DistanceMarker[] DistanceMarkers;
     
        [XmlArrayItem("Lap")]
        public Lap[] Laps;

        public GPSRoute GPSRoute;

    }
}
