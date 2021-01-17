using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Models;

namespace ZTP.Patterns.Strategy
{
    public interface IBillingStrategy
    {
        void constructDocument(InvoiceBuilder invoiceBuilder, Order order, List<ProductDecorator> shoppingCartDecoratorsList, List<Product> shoppingCartList);
    }
}
