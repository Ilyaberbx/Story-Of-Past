using System.Collections.Generic;

namespace Odumbrata.Core
{
    public interface IRegister<TElement>
    {
        public void Add(TElement element);
        public void Remove(TElement element);
        public bool Contains(TElement element);
        IReadOnlyList<TElement> Elements { get; }
    }
}