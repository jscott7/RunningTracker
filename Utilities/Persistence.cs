using SportTracksXmlReader;
using System.Xml.Serialization;
using System.Xml;

namespace Utilities
{
    public class Persistence
    {
        public static Logbook? LoadLogbook(string logbookPath)
        {
            if (string.IsNullOrEmpty(logbookPath))
            {
                throw new ArgumentException("Logbook path cannot be null or empty.", nameof(logbookPath));
            }

            if (!File.Exists(logbookPath))
            {
                throw new FileNotFoundException("Logbook file not found.", logbookPath);
            }

            var xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load(logbookPath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to load the XML document from {logbookPath}.", ex);
            }

            var documentElement = xmlDocument.DocumentElement ?? throw new InvalidOperationException("Failed to get the document element from the XML document.");
            var nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nsmgr.AddNamespace("ns", "urn:uuid:D0EB2ED5-49B6-44e3-B13C-CF15BE7DD7DD");

            XmlSerializer xmlSerializer = new(typeof(Logbook));

            try
            {
                using var reader = new XmlNodeReader(documentElement);
                return xmlSerializer.Deserialize(reader) as Logbook;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Failed to deserialize the XML document into a Logbook object.", ex);
            }
        }

        public static void SaveLogbook(Logbook? logbook, string logbookPath)
        {
            if (logbook == null)
            {
                throw new ArgumentNullException(nameof(logbook), "Logbook cannot be null.");
            }

            if (string.IsNullOrEmpty(logbookPath))
            {
                throw new ArgumentException("Logbook path cannot be null or empty.", nameof(logbookPath));
            }

            var xmlSerializer = new XmlSerializer(typeof(Logbook));
            var xmlDocument = new XmlDocument();
            using (var memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, logbook);
                memoryStream.Position = 0;
                xmlDocument.Load(memoryStream);
            }

            try
            {
                xmlDocument.Save(logbookPath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to save the Logbook to {logbookPath}.", ex);
            }
        }
    }
}
