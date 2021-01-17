using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Patterns
{
    public class BubbleWrap : ProductDecorator
    {
        public BubbleWrap(ProductBase p) : base(p) { }

        public override decimal getPrice()
        {
            return base.getPrice() + 5;
        }

        public override string getDescription()
        {
            return base.getDescription() + "Dodatek: Folia bąbelkowa + 5zł\n";
        }
    }
}
