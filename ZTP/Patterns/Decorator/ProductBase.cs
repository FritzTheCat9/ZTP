using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Patterns
{
    public abstract class ProductBase
    {
        public abstract decimal getPrice();
        public abstract string getDescription();
    }
}
