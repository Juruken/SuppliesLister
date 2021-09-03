using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SuppliesPriceLister.Core.Model;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace SuppliesPriceLister.Core
{
    public class JsonFileReader<T> : IFileReader<Partner>
    {
        private readonly decimal _conversionRate;

        public JsonFileReader(decimal conversionRate)
        {
            _conversionRate = conversionRate;
        }

        public IEnumerable<Partner> ReadFile(string fileName)
        {
            var partners = new List<Partner>();
            
            // Don't read a huge file into memory, break into smaller chunks if needed
            using (var file = File.OpenText(fileName))
            using (var reader = new JsonTextReader(file))
            {
                reader.SupportMultipleContent = true;

                var serializer = new JsonSerializer();
                while (reader.Read())
                {
                    if (reader.TokenType != JsonToken.StartObject) continue;

                    var data = serializer.Deserialize<PartnerFile>(reader);

                    if (data == null)
                    {
                        throw new InvalidDataException($"Failed to read partner data from {fileName}");
                    }
                    
                    partners.AddRange(data.Partners.ToList());
                }
            }
            
            partners.ForEach(p => p.Supplies.ToList().ForEach(s =>
            {
                // This would ideally be dealt with elsewhere as it assumes all Json files are in USD
                // Convert from Cents and USD to AUD
                s.Price = Math.Round(s.Price / 100 / _conversionRate, 2);
            }));

            return partners;
        }
    }
}