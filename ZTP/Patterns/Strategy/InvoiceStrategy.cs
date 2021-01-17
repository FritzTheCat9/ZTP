using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Models;

namespace ZTP.Patterns.Strategy
{
    public class InvoiceStrategy : IBillingStrategy     // faktury
    {
        public void constructDocument(InvoiceBuilder invoiceBuilder, Order order, List<ProductDecorator> shoppingCartDecoratorsList, List<Product> shoppingCartList)
        {
            invoiceBuilder.AddInvoiceTitle();
            invoiceBuilder.AddSellerInfo();
            invoiceBuilder.AddDate();
            invoiceBuilder.AddCustomerInfo(order.Customer);
            invoiceBuilder.AddPrice(order.Price);
            invoiceBuilder.AddPaymentMethodInfo(order.PaymentMethod);
            invoiceBuilder.AddShippingMethodInfo(order.ShippingMethod);
            invoiceBuilder.AddProductsInfo(shoppingCartList.ToList(), shoppingCartDecoratorsList);
        }
    }
}
