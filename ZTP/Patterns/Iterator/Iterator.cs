using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ZTP.Patterns.Iterator
{
    abstract class Iterator : IEnumerator
    {
        object IEnumerator.Current => Current();

        public abstract int Key();

        public abstract object Current();

        public abstract bool MoveNext();

        public abstract void Reset();
    }
}
