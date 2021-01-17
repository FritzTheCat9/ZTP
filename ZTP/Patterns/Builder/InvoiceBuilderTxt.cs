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
            innvoiceText += "\n";
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
            innvoiceText += "\n";
            innvoiceText += "Name: " + paymentMethod.Name + "\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "\n";
        }

        public void AddProductsInfo(List<Product> products)
        {
            innvoiceText += "\n";
            innvoiceText += "------------------------------------------------- \n";

            innvoiceText += "Ordered products: \n";
            innvoiceText += "\n";

            foreach (var product in products)
            {
                innvoiceText += "Name: " + product.Name + "\n";
                innvoiceText += "Price: " + product.Price + "\n";
                innvoiceText += "VAT: " + product.VAT + "\n";
            }

            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "\n";
        }

        public void AddSellerInfo()
        {
            innvoiceText += "\n";
            innvoiceText += "------------------------------------------------- \n";
            innvoiceText += "Seller Info: \n";
            innvoiceText += "\n";
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
            innvoiceText += "\n";
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