using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Models;

namespace ZTP.Patterns.Strategy
{
    public class ReceiptStrategy : IBillingStrategy     // paragony
    {
        public void constructDocument(InvoiceBuilder invoiceBuilder, Order order, List<ProductDecorator> shoppingCartDecoratorsList, List<Product> shoppingCartList)
        {
            invoiceBuilder.AddReceiptTitle();
            invoiceBuilder.AddSellerInfo();
            invoiceBuilder.AddDate();
            invoiceBuilder.AddPrice(order.Price);
            invoiceBuilder.AddProductsInfo(shoppingCartList.ToList(), shoppingCartDecoratorsList);
        }
    }
}
