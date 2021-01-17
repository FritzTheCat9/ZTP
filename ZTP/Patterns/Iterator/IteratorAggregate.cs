using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;

namespace ZTP.Patterns.Iterator
{
    abstract class IteratorAggregate : IEnumerable
    {
        
        public abstract IEnumerator GetEnumerator();
    }
}
