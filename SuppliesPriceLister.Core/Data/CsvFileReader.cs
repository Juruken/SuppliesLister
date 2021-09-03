using System.Collections.Generic;
using System.IO;
using SuppliesPriceLister.Core.Model;

namespace SuppliesPriceLister.Core
{
    public class CsvFileReader<T> : IFileReader<Supply> where T: Supply
    {
        public IEnumerable<Supply> ReadFile(string fileName)
        {
            var supplies = new List<Supply>();

            using (var reader = new StreamReader(fileName))
            {
                // Skip header line
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line?.Split(",");

                    if (values == null || values.Length <= 3)
                    {
                        throw new InvalidDataException($"Invalid Supply data found in {fileName}");
                    }
                    
                    supplies.Add(new Supply
                    {
                        Id = values[0],
                        Description = values[1],
                        Unit = values[2],
                        Price = decimal.Parse(values[3])
                    });
                }
            }

            return supplies;
        }
    }
}