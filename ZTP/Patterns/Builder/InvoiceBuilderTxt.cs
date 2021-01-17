﻿using System;
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

        public void AddPaymentMethodInfo(PaymentMethod paymentMethod)
        {
            innvoiceText += "\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "PaymentMethod: \n";
            innvoiceText += "Name: " + paymentMethod.Name + "\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "\n";
        }

        public void AddPrice(decimal price, decimal packagesPrice)
        {
            var sumPrice = price + packagesPrice;

            innvoiceText += "\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "Order Price: " + sumPrice + "zł\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "\n";
        }

        public void AddProductsInfo(List<Product> products, IList<ProductDecorator> shoppingCartDecoratorsList)
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
                innvoiceText += "Packages: " + shoppingCartDecoratorsList[i].getDescription() + "\n";
            }

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
