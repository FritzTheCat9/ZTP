using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Models;

namespace ZTP.Patterns
{
    public class InvoiceBuilderTxt : InvoiceBuilder
    {
        private string innvoiceText;

        public void AddCustomerInfo(Customer customer)
        {
            innvoiceText += "\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "Customer: \n";
            innvoiceText += "FirstName: " + customer.FirstName + "\n";
            innvoiceText += "LastName: " + customer.LastName + "\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "\n";
        }

        public void AddDate()
        {
            innvoiceText += "\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "Date: " + DateTime.Now.ToString() + "\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "\n";
        }

        public void AddInvoiceTitle()
        {
            innvoiceText += "\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "INVOICE\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "\n";
        }

        public void AddPaymentMethodInfo(PaymentMethod paymentMethod)
        {
            innvoiceText += "\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "PaymentMethod: \n";
            innvoiceText += "Name: " + paymentMethod.Name + "\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "\n";
        }

        public void AddPrice(decimal price)
        {
            innvoiceText += "\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "Order Price: " + price + "zł\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "\n";
        }

        public void AddProductsInfo(List<Product> products, List<ProductDecorator> shoppingCartDecoratorsList)
        {
            innvoiceText += "\n";
            innvoiceText += "------------------------------------------------- \n";

            innvoiceText += "Ordered products: \n";
            innvoiceText += "\n";

            for (int i = 0; i < products.Count; i++)
            {
                innvoiceText += "Name: " + products[i].Name + "\n";
                innvoiceText += "Price: " + products[i].Price + "zł\n";
                innvoiceText += "VAT: " + products[i].VAT + "%\n";
                if(shoppingCartDecoratorsList.Count > 0)
                {
                    innvoiceText += "Packages: " + shoppingCartDecoratorsList[i].getDescription() + "\n";
                }
            }

            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "\n";
        }

        public void AddReceiptTitle()
        {
            innvoiceText += "\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "RECEIPT\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "\n";
        }

        public void AddSellerInfo()
        {
            innvoiceText += "\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "Seller Info: \n";
            innvoiceText += "FirstName: Robert" + "\n";
            innvoiceText += "LastName: Lewandowski" + "\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "\n";
        }

        public void AddShippingMethodInfo(ShippingMethod shippingMethod)
        {
            innvoiceText += "\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "ShippingMethod: \n";
            innvoiceText += "Name: " + shippingMethod.Name + "\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "\n";
        }

        public string GetInvoiceInTxt()
        {
            return innvoiceText;
        }
    }
}
