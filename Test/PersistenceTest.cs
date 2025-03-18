using NUnit.Framework;
using SportTracksXmlReader;
using System.IO;
using Utilities;

namespace UnitTests
{
    class PersistenceTest
    {
        [Test]
        public void valid_logbook_loads()
        {
            var logbookPath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "Valid.logbook");
            var logbook = Persistence.LoadLogbook(logbookPath);
            Assert.That(logbook, Is.Not.Null);
            Assert.That(logbook.Activities, Has.Length.EqualTo(1));
            Assert.That(logbook.Activities[0].location, Is.EqualTo("Chislehurst"));
        }

        [Test]
        public void missing_logbook_throws_exception()
        {
            var missingLogbook = "Missing.logbook";
            var logbookPath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", missingLogbook);
            Assert.That(() => Persistence.LoadLogbook(logbookPath),
                Throws.TypeOf<FileNotFoundException>()
                .With.Property("FileName").Contains(missingLogbook));
        }

        [Test]
        public void empty_logbook_path_throws_exception()
        {
            Assert.That(() => Persistence.LoadLogbook(string.Empty), Throws.TypeOf<System.ArgumentException>());
        }

        [Test]
        public void null_logbook_path_throws_exception()
        {
            Assert.That(() => Persistence.LoadLogbook(null), Throws.TypeOf<System.ArgumentException>());
        }

        [Test]
        public void invalid_xml_file_throws_exception()
        {
            var logbookPath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "Invalid.logbook");

            Assert.That(() => Persistence.LoadLogbook(logbookPath),
                Throws.TypeOf<System.InvalidOperationException>()
                .With.InnerException);
        }
    }
}
