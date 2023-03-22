using SportTracksXmlReader;
using System;
using System.Xml;
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
            var logbook = LoadLogbook(logbookFile);

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

        static Logbook LoadLogbook(string logbookPath)
        {         
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(logbookPath);

            var documentElement = xmlDocument.DocumentElement;
            var nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nsmgr.AddNamespace("ns", "urn:uuid:D0EB2ED5-49B6-44e3-B13C-CF15BE7DD7DD");

            var xmlSerializer = new XmlSerializer(typeof(Logbook));

            try
            {
                return(Logbook)xmlSerializer.Deserialize(new XmlNodeReader((XmlNode)documentElement));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
