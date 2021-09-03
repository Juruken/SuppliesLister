using System;
using System.Collections.Generic;
using System.Linq;
using SuppliesPriceLister.Core;
using SuppliesPriceLister.Core.Model;
using SuppliesPriceLister.Core.Output;

namespace SuppliesPriceLister
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Read conversion rate from config
            var jsonReader = new JsonFileReader<Partner>(0.7m);
            var csvReader = new CsvFileReader<Supply>();

            var partners = jsonReader.ReadFile("megacorp.json");
            var supplies = csvReader.ReadFile("humphries.csv");

            var combinedSupplies = supplies.ToList();
            // TODO: Handle duplicates using dictionary supply.id => supply
            partners.ToList().ForEach(p => combinedSupplies.AddRange(p.Supplies.ToList()));

            var lister = new SuppliesLister<Supply>(new ConsoleOutput());
            Print(lister, combinedSupplies);
            Console.ReadLine();
        }

        // Ideally this would be using AutoFac (or similar) and dependency injection within another class/file rather than hard coded above 
        static void Print(ILister<Supply> lister, IEnumerable<Supply> data)
        {
            lister.List(data);
        }
    }
}
