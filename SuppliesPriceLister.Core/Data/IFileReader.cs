using System.Collections.Generic;

namespace SuppliesPriceLister.Core
{
    public interface IFileReader<T>
    {
        IEnumerable<T> ReadFile(string fileName);
    }
}
