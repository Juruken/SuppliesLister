using System.Collections.Generic;
using System.Linq;
using SuppliesPriceLister.Core.Model;
using SuppliesPriceLister.Core.Output;

namespace SuppliesPriceLister.Core
{
    public class SuppliesLister<T> : ILister<Supply> where T: Supply
    {
        private readonly IOutput _output;

        public SuppliesLister(IOutput output)
        {
            _output = output;
        }

        public void List(IEnumerable<Supply> data)
        {
            var supplies = data.ToList();
            supplies.Sort((a, b) => a.Price < b.Price ? 1 : -1);

            supplies.ForEach(s => _output.Write($"{s.Id}, {s.Description}, ${s.Price}"));
        }
    }
}