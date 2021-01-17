using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Patterns
{
    public class EmptyDecorator : ProductDecorator
    {
        public EmptyDecorator(ProductBase p) : base(p) { }

        public override decimal getPrice()
        {
            return base.getPrice();
        }

        public override string getDescription()
        {
            return base.getDescription();
        }
    }
}
