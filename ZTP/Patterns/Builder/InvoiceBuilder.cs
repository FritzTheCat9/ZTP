using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Models;

namespace ZTP.Patterns
{
    public interface InvoiceBuilder
    {
        void AddCustomerInfo(Customer customer);
        void AddShippingMethodInfo(ShippingMethod shippingMethod);
        void AddPaymentMethodInfo(PaymentMethod paymentMethod);
        void AddProductsInfo(List<Product> products, IList<ProductDecorator> shoppingCartDecoratorsList);
        void AddSellerInfo();
        void AddPrice(decimal price);
    }
}
