using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Patterns
{
    public class PlasticBox : ProductDecorator
    {
        public PlasticBox(ProductBase p) : base(p) { }

        public override decimal getPrice()
        {
            return base.getPrice() + 15;
        }

        public override string getDescription()
        {
            return base.getDescription() + "Dodatek: Plastikowe pudełko \n";
        }
    }
}
