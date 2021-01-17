using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Patterns
{
    public class CardboardBox : ProductDecorator
    {
        public CardboardBox(ProductBase p) : base(p) { }

        public override decimal getPrice()
        {
            return base.getPrice() + 10;
        }

        public override string getDescription()
        {
            return base.getDescription() + "Dodatek: Kartonowe pudełko + 10zł\n";
        }
    }
}
