using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Patterns
{
    public class ProductDecorator : ProductBase
    {
        ProductBase product;

        public ProductDecorator(ProductBase p)
        {
            product = p;
        }

        public override string getDescription()
        {
            return product.getDescription();
        }

        public override decimal getPrice()
        {
            return product.getPrice();
        }
    }
}
