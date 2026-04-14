using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SportTracksXmlReader
{
    public class ActivityCategory
    {
        public Metadata Metadata;

        [XmlAttribute(AttributeName = "referenceId")]
        public Guid ReferenceId;
     
        [XmlAttribute(AttributeName = "name")]
        public string Name;

        [XmlAttribute(AttributeName = "useParentSettings")]
        public bool UseParentSettings;

        [XmlAttribute(AttributeName = "useSystemLengthUnits")]
        public bool UseSystemLengthUnits;

        [XmlAttribute(AttributeName = "stoppedSpeed")]
        public float StoppedSpeed;

        [XmlAttribute(AttributeName = "speedUnits")]
        public string SpeedUnits;

        [XmlAttribute(AttributeName = "speedZone")]
        public Guid SpeedZone { get; set; }
        public bool ShouldSerializeSpeedZone() => SpeedZone != Guid.Empty;

        [XmlAttribute(AttributeName = "heartRateZone")]
        public Guid HeartRateZone { get; set; }
        public bool ShouldSerializeHeartRateZone() => HeartRateZone != Guid.Empty;

        [XmlAttribute(AttributeName = "calorieCalculationMethod")]
        public Guid CalorieCalculationMethod { get; set; }
        public bool ShouldSerializeCalorieCalculationMethod() => CalorieCalculationMethod != Guid.Empty;

        [XmlArrayItem("ActivityCategory")]
        public ActivityCategory[] Categories;
    }
}
