using System;

namespace SuppliesPriceLister.Core.Output
{
    public class ConsoleOutput : IOutput
    {
        public void Write(string data)
        {
            Console.WriteLine(data);
        }
    }
}