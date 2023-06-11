using SportTracksXmlReader;
using System;
using System.Xml.Serialization;
using Utilities;

namespace SportTracksClient
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Missing logbook file");
                return;
            }

            var logbookFile = args[0];
            var logbook = Persistence.LoadLogbook(logbookFile);

            // Optional load fit file and add to logbook
            if (args.Length > 1)
            {
                var fitFilePath = args[1];
                var fitDeserializer = new FitDeserializer();
                fitDeserializer.DeserializeAndAddToLogbook(logbook, fitFilePath);
            }

            // Save the logbook back to temp file
            var xmlSerializer = new XmlSerializer(typeof(Logbook));
            using (var sr = new System.IO.StreamWriter(@"C:\temp\logbook.xml"))
            {
                xmlSerializer.Serialize(sr, logbook);
            }

   /*         foreach (var activity in logbook.Activities)
            {
                if (activity.GPSRoute != null)
                {
                    activity.GPSRoute.DecodeBinaryData();
                    Console.WriteLine($"Elevation data count: {activity.GPSRoute.ElevationData.Count}");
                }
            }*/
            Console.WriteLine("Finished");
        }


    }
}
