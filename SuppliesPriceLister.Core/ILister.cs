using System.Collections.Generic;

namespace SuppliesPriceLister.Core
{
    public interface ILister<T>
    {
        void List(IEnumerable<T> data);
    }
}