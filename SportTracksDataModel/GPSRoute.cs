using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SportTracksXmlReader
{
    public class GPSRoute
    {
        public TrackData TrackData;

        [XmlIgnore]
        public List<DateTime> TimeData = new List<DateTime>();

        [XmlIgnore]
        public List<float> LongitudeData = new List<float>();

        [XmlIgnore]
        public List<float> LatitudeData = new List<float>();

        [XmlIgnore]
        public List<float> ElevationData = new List<float>();

        [XmlIgnore]
        public List<uint> ElapsedSecondsPerStep = new List<uint>();
   
        [XmlIgnore]
        public List<DateTime> StartTime = new List<DateTime>();

        [XmlIgnore]
        public List<float> DistanceData = new List<float>();

        private static DateTime trackDataTimeEpoch = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc);
 
        public GPSRoute()
        {
            TrackData = new TrackData { Version = 4 };
        }

        public void DecodeBinaryData()
        {
            ReadBinarySegments(Convert.FromBase64String(TrackData.Data));
        }

        public void EncodeBinaryData()
        {
            WriteBinary();
        }

        private void WriteBinary()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter((Stream)memoryStream))
                {
                    DateTime startTime = TimeData[0];
                    int startIndex = 0;
                   
                    int index;
                    uint lastElapsedSeconds = 0;
                    for (index = 1; index < LongitudeData.Count; ++index)
                    {
                        // Older persisted data flushed the data when the difference in seconds was >= 254 bytes
                        // This check is in place to handle those 
                        if (ElapsedSecondsPerStep[index] == 0 || (ElapsedSecondsPerStep[index] - lastElapsedSeconds) > (uint)byte.MaxValue || index - startIndex > 254)
                        {
                            FlushSegment(writer, startIndex, index - 1);
                            startIndex = index;
                            lastElapsedSeconds = ElapsedSecondsPerStep[index];
                        }
                    }

                    FlushSegment(writer, startIndex, LongitudeData.Count - 1);
                    writer.Write(0L);

                    byte[] array = memoryStream.ToArray();
                    TrackData.Data = Convert.ToBase64String(array, 0, array.Length);
                }
            }
        }

        private void FlushSegment(
           BinaryWriter writer,
           int startIndex,
           int endIndex)
        {
            if (startIndex > endIndex)
                return;

            uint elapsedSeconds = 0; //route[startIndex].ElapsedSeconds;
            long totalSeconds = (long)(StartTime[startIndex].AddSeconds((double)elapsedSeconds) - trackDataTimeEpoch).TotalSeconds;
         
            writer.Write(totalSeconds);
            byte num1 = (byte)(endIndex - startIndex + 1);
            writer.Write(num1);
            for (int index = startIndex; index <= endIndex; ++index)
            {
                byte num2 = (byte)ElapsedSecondsPerStep[index];
                writer.Write(num2);
                writer.Write(LatitudeData[index]);
                writer.Write(LongitudeData[index]);
                writer.Write(ElevationData[index]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>true for successful read</returns>
        /// <remarks>Call with ReadBinarySegments(Convert.FromBase64String(x));</remarks>
        private bool ReadBinarySegments(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                using (BinaryReader binaryReader = new BinaryReader((Stream)memoryStream))
                {
                    try
                    {
                        for (long index1 = binaryReader.ReadInt64(); index1 != 0L; index1 = binaryReader.ReadInt64())
                        {
                            DateTime startDateTime = trackDataTimeEpoch;
                            startDateTime = startDateTime.AddSeconds((double)index1);
                        
                            byte num = binaryReader.ReadByte();
                            for (byte index2 = 0; (int)index2 < (int)num; ++index2)
                            {
                                var elapsedSeconds = (uint)binaryReader.ReadByte();
                                ElapsedSecondsPerStep.Add(elapsedSeconds);
                             
                                var elapsedTimeForThisStep = startDateTime.AddSeconds(elapsedSeconds);
                                TimeData.Add(elapsedTimeForThisStep);
                        
                                StartTime.Add(startDateTime);

                                LatitudeData.Add(binaryReader.ReadSingle());
                                LongitudeData.Add(binaryReader.ReadSingle());
                                ElevationData.Add(binaryReader.ReadSingle());
                            }
                        }
                    }
                    catch(Exception)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
