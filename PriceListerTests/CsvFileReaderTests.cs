using System.Linq;
using NUnit.Framework;
using SuppliesPriceLister.Core;
using SuppliesPriceLister.Core.Model;

namespace SuppliesPriceLister.Tests
{
    [TestFixture]
    public class CsvFileReaderTests
    {
        private IFileReader<Supply> _fileReader;

        [SetUp]
        public void Setup()
        {
            _fileReader = new CsvFileReader<Supply>();
        }

        [TestCase("data/humphries.csv")]
        public void ReadFile(string fileName)
        {
            var supplies = _fileReader.ReadFile(fileName);
            Assert.IsNotEmpty(supplies);
            Assert.AreEqual(supplies.Count(), 10);
            
            var tm3R11Mesh = supplies.FirstOrDefault(s => s.Description == "TM3 R11 Mesh");
            Assert.IsNotNull(tm3R11Mesh);
            Assert.AreEqual(tm3R11Mesh.Unit, "m2");
            Assert.AreEqual(tm3R11Mesh.Price, 83.99);
            Assert.AreEqual(tm3R11Mesh.Id, "e1b3e145-782b-43b3-a081-f3634a07db00");
        }
    }
}