using SportTracksXmlReader;
using System.Xml.Serialization;
using System.Xml;

namespace Utilities
{
    public class Persistence
    {
        public static Logbook LoadLogbook(string logbookPath)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(logbookPath);

            var documentElement = xmlDocument.DocumentElement;

            var nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nsmgr.AddNamespace("ns", "urn:uuid:D0EB2ED5-49B6-44e3-B13C-CF15BE7DD7DD");

            XmlSerializer xmlSerializer = new (typeof(Logbook));

            Logbook output = null;
            try
            {
                output = xmlSerializer.Deserialize(new XmlNodeReader(documentElement)) as Logbook;
            }
            catch(Exception ex)
            {
                //TODO: Handle this exception
                string x = "y";
            }

            return output;
        }
    }
}
