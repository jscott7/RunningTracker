using Dynastream.Fit;
using Microsoft.VisualBasic;
using SportTracksXmlReader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Activity = SportTracksXmlReader.Activity;

namespace Utilities
{
    public class FitDeserializer
    {
        private FileStream _fitSource;
        private Activity _activity = new Activity();
        private Dictionary<ushort, int> mesgCounts = new Dictionary<ushort, int>();

        public Activity Activity { get { return _activity; } }
        public bool DeserializeAndAddToLogbook(Logbook logbook, string fitFilePath)
        {
            bool status = false;
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            try
            {
                status = Deserialize(fitFilePath);

                BuildLapData();

                logbook.Activities = logbook.Activities.Append(_activity).ToArray();
                stopwatch.Stop();
                //TODO We need to update the current active equipment item with the reference to this activity
                //linq on InUse field

                Console.WriteLine("");
                Console.WriteLine("Time elapsed: {0:0.#}s", stopwatch.Elapsed.TotalSeconds);
            }
            catch (FitException ex)
            {
                Console.WriteLine("A FitException occurred when trying to decode the FIT file. Message: " + ex);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred when trying to decode the FIT file. Message: " + ex);
                return false;
            }

            return status;
        }

        public bool Deserialize(string fitFilePath)
        {
            bool status = false;
            try
            {
                // Attempt to open .FIT file
                using (_fitSource = new FileStream(fitFilePath, FileMode.Open))
                {
                    Console.WriteLine("Opening {0}", fitFilePath);

                    var decodeDemo = new Decode();
                    var mesgBroadcaster = new MesgBroadcaster();

                    // Connect the Broadcaster to our event (message) source (in this case the Decoder)
                    decodeDemo.MesgEvent += mesgBroadcaster.OnMesg;
                    decodeDemo.MesgDefinitionEvent += mesgBroadcaster.OnMesgDefinition;
                    // decodeDemo.DeveloperFieldDescriptionEvent += OnDeveloperFieldDescriptionEvent;

                    // Subscribe to message events of interest by connecting to the Broadcaster
                    mesgBroadcaster.MesgEvent += OnMesg;
                    // mesgBroadcaster.MesgDefinitionEvent += OnMesgDefn;

                    //  mesgBroadcaster.FileIdMesgEvent += OnFileIDMesg;
                    //  mesgBroadcaster.UserProfileMesgEvent += OnUserProfileMesg;
                    //  mesgBroadcaster.MonitoringMesgEvent += OnMonitoringMessage;
                    //  mesgBroadcaster.DeviceInfoMesgEvent += OnDeviceInfoMessage;
                    mesgBroadcaster.RecordMesgEvent += OnRecordMessage;

                    status = decodeDemo.IsFIT(_fitSource);
                    status &= decodeDemo.CheckIntegrity(_fitSource);

                    // Process the file
                    if (status)
                    {
                        Console.WriteLine("Decoding...");
                        decodeDemo.Read(_fitSource);
                        Console.WriteLine("Decoded FIT file {0}", fitFilePath);
                    }
                    else
                    {
                        try
                        {
                            Console.WriteLine("Integrity Check Failed {0}", fitFilePath);
                            if (decodeDemo.InvalidDataSize)
                            {
                                Console.WriteLine("Invalid Size Detected, Attempting to decode...");
                                decodeDemo.Read(_fitSource);
                            }
                            else
                            {
                                Console.WriteLine("Attempting to decode by skipping the header...");
                                decodeDemo.Read(_fitSource, DecodeMode.InvalidHeader);
                            }
                        }
                        catch (FitException ex)
                        {
                            Console.WriteLine("DecodeDemo caught FitException: " + ex.Message);
                        }
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Summary:");
                    int totalMesgs = 0;
                    foreach (KeyValuePair<ushort, int> pair in mesgCounts)
                    {
                        Console.WriteLine("MesgID {0,3} Count {1}", pair.Key, pair.Value);
                        totalMesgs += pair.Value;
                    }

                    Console.WriteLine("{0} Message Types {1} Total Messages", mesgCounts.Count, totalMesgs);

                    _activity.GPSRoute.EncodeBinaryData();

                    // These will be added by the user
                    _activity.CategoryName = "Running";
                    _activity.location = "ATestLocation";

                    var currentTimeStamp = System.DateTime.Now;
                    _activity.Metadata = new Metadata() { Created = currentTimeStamp, Modified = currentTimeStamp };
                }
            }
            catch (FitException ex)
            {
                Console.WriteLine("A FitException occurred when trying to decode the FIT file. Message: " + ex);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred when trying to decode the FIT file. Message: " + ex);
                return false;
            }

            return status;
            
        }

        private void BuildLapData()
        {
       
            var laps = new List<Lap>();

            var nextLapDistance = 1000;
            var startIndex = 0;
            for (int distanceIndex = 0; distanceIndex < _activity.GPSRoute.DistanceData.Count; distanceIndex++)
            {
                if (_activity.GPSRoute.DistanceData[distanceIndex] >= nextLapDistance)
                {
                    var lap = new Lap();
                    lap.TotalDistance = nextLapDistance;
                    lap.TotalTime = (float)(_activity.GPSRoute.TimeData[distanceIndex] - _activity.GPSRoute.TimeData[startIndex]).TotalSeconds;
                    lap.StartTime = _activity.GPSRoute.TimeData[startIndex];
                    startIndex = distanceIndex;
                    nextLapDistance += 1000;
                    laps.Add(lap);
                }

                // Add fractional last lap if required
                if (distanceIndex == _activity.GPSRoute.DistanceData.Count - 1 && distanceIndex != startIndex)
                {
                    var lap = new Lap();
                    lap.TotalDistance = _activity.GPSRoute.DistanceData[distanceIndex] - (nextLapDistance - 1000);
                    lap.TotalTime = (float)(_activity.GPSRoute.TimeData[distanceIndex] - _activity.GPSRoute.TimeData[startIndex]).TotalSeconds;
                    lap.StartTime = _activity.GPSRoute.TimeData[startIndex];
                    startIndex = distanceIndex;
                    laps.Add(lap);
                }
            }

            _activity.Laps = laps.ToArray();
        }

        void OnMesg(object sender, MesgEventArgs e)
        {
            Console.WriteLine("OnMesg: Received Mesg with global ID#{0}, its name is {1}", e.mesg.Num, e.mesg.Name);

            if (e.mesg.Name == "Session")
            {
                DecodeActivitySummary(e.mesg.Fields);
            }
            int i = 0;
            foreach (Field field in e.mesg.Fields)
            {
                for (int j = 0; j < field.GetNumValues(); j++)
                {
                    Console.WriteLine("\tField{0} Index{1} (\"{2}\" Field#{4}) Value: {3} (raw value {5})",
                        i,
                        j,
                        field.GetName(),
                        field.GetValue(j),
                        field.Num,
                        field.GetRawValue(j));
                }

                i++;
            }

            foreach (var devField in e.mesg.DeveloperFields)
            {
                for (int j = 0; j < devField.GetNumValues(); j++)
                {
                    Console.WriteLine("\tDeveloper{0} Field#{1} Index{2} (\"{3}\") Value: {4} (raw value {5})",
                        devField.DeveloperDataIndex,
                        devField.Num,
                        j,
                        devField.Name,
                        devField.GetValue(j),
                        devField.GetRawValue(j));
                }
            }

            if (mesgCounts.ContainsKey(e.mesg.Num))
            {
                mesgCounts[e.mesg.Num]++;
            }
            else
            {
                mesgCounts.Add(e.mesg.Num, 1);
            }
        }

        /*
           * A semicircle is a unit of location-based measurement on an arc. 
           * An arc of 180 degrees is made up of many semicircle units; 2^31 semicircles to be exact. 
           * A semicircle can be represented by a 32-bit number. This number can hold more information and is more accurate for GPS systems than a simple longitude and latitude value.
           * Semicircles that correspond to North latitudes and East longitudes are indicated with positive values; 
           * semicircles that correspond to South latitudes and West longitudes are indicated with negative values.
           *   The following formulas show how to convert between degrees and semicircles:
           *   degrees = semicircles * ( 180 / 2^31 )
           */
        private void OnRecordMessage(object sender, MesgEventArgs e)
        {
            Console.WriteLine("Record Handler: Received {0} Mesg, it has global ID#{1}",
                e.mesg.Num,
                e.mesg.Name);

            var recordMessage = (RecordMesg)e.mesg;

            if (_activity.GPSRoute == null)
            {
                _activity.GPSRoute = new GPSRoute();
            }

            var lat = recordMessage.GetPositionLat();
            var longit = recordMessage.GetPositionLong();
            var distance = recordMessage.GetDistance();
    
            var latDegrees = GPSLib.CoordinateConversion.SemicircleToDegrees(lat);
            var longDegrees = GPSLib.CoordinateConversion.SemicircleToDegrees(longit);

            var alt = recordMessage.GetAltitude();
            var timestamp = recordMessage.GetTimestamp();

            if (_activity.GPSRoute.StartTime.Count == 0)
            {
                _activity.GPSRoute.StartTime.Add(timestamp.GetDateTime()); 
                _activity.GPSRoute.ElapsedSecondsPerStep.Add(0);
            }
            else
            {        
                var elapsedSecondsPerStep = (timestamp.GetDateTime() - _activity.GPSRoute.TimeData[_activity.GPSRoute.TimeData.Count - 1]).TotalSeconds;
                var lastElapsedSecondsPerStep = _activity.GPSRoute.ElapsedSecondsPerStep.Last();
                var elapsedSeconds = (uint)Convert.ToInt32(elapsedSecondsPerStep) + lastElapsedSecondsPerStep;

                if (elapsedSecondsPerStep - lastElapsedSecondsPerStep > (uint)byte.MaxValue || elapsedSeconds > (uint)byte.MaxValue)
                {
                    _activity.GPSRoute.ElapsedSecondsPerStep.Add(0);
                    _activity.GPSRoute.StartTime.Add(timestamp.GetDateTime());
                }
                else
                {
                    _activity.GPSRoute.ElapsedSecondsPerStep.Add(elapsedSeconds);
                    _activity.GPSRoute.StartTime.Add(_activity.GPSRoute.StartTime.Last());
                }                  
            }

            _activity.GPSRoute.LatitudeData.Add(latDegrees);
            _activity.GPSRoute.LongitudeData.Add(longDegrees);
            _activity.GPSRoute.TimeData.Add(timestamp.GetDateTime());
            _activity.GPSRoute.ElevationData.Add(alt.Value);
            _activity.GPSRoute.DistanceData.Add(distance.Value);
        }

        void DecodeActivitySummary(IEnumerable<Field> summaryFields)
        {
            _activity.ReferenceId = Guid.NewGuid();
            _activity.CategoryId = Guid.NewGuid(); // Although this will be the "running" category
            _activity.TotalDistance = (float)GetSummaryValue(summaryFields, "TotalDistance");
            _activity.TotalCalories = (ushort)GetSummaryValue(summaryFields, "TotalCalories");
            _activity.TotalTime = (float)GetSummaryValue(summaryFields, "TotalElapsedTime");

            var startTime = GetSummaryValue(summaryFields, "StartTime");

            var x = new Dynastream.Fit.DateTime((uint)startTime);
            _activity.StartTime = x.GetDateTime();
            if (_activity.StartTime != null)
            {
                _activity.HasStartTime = true;
            }
        }

        object GetSummaryValue(IEnumerable<Field> summaryFields, string fieldName)
        {
            var field = from Field item in summaryFields
                        where item.Name == fieldName
                        select item;

            return field.FirstOrDefault().GetValue();
        }
    }
}
