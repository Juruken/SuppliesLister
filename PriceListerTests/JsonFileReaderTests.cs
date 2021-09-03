using System;
using System.Linq;
using NUnit.Framework;
using SuppliesPriceLister.Core;
using SuppliesPriceLister.Core.Model;

namespace SuppliesPriceLister.Tests
{
    [TestFixture]
    public class JsonFileReaderTests
    {
        private IFileReader<Partner> _fileReader;

        [SetUp]
        public void Setup()
        {
            _fileReader = new JsonFileReader<Partner>(0.7m);
        }

        [TestCase("data/megacorp.json")]
        public void ReadFile(string fileName)
        {
            var partners = _fileReader.ReadFile(fileName);
            Assert.IsNotNull(partners);
            Assert.IsNotEmpty(partners);
            Assert.AreEqual(partners.Count(), 2);

            var southEast = partners.FirstOrDefault(p => p.Name == "Megacorp Southeast");

            Assert.IsNotNull(southEast);
            Assert.AreEqual(southEast.Address, "14 Park Crescent, Clayton");
            Assert.AreEqual(southEast.Type, "INTERNAL");
            Assert.AreEqual(southEast.Supplies.ToList().Count, 7);

            var internalBeam = southEast.Supplies.FirstOrDefault(s => s.Id == "1");
            Assert.IsNotNull(internalBeam);
            Assert.AreEqual(internalBeam.Description, "100 x 200 x 20mpa Internal Beam");
            Assert.AreEqual(internalBeam.Price, 57.14m);
            Assert.AreEqual(internalBeam.Unit, "lm");
            Assert.AreEqual(internalBeam.ProviderId, new Guid("907d853f-dbe7-45c0-8e59-9dff4044cf80"));
            Assert.AreEqual(internalBeam.MaterialType, "Steel");
        }
    }
}
