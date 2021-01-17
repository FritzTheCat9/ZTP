﻿using SautinSoft.Document;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Patterns;

namespace ZTP.Models
{
    public class InvoiceBuilderPdf : InvoiceBuilder
    {
        DocumentCore dc = new DocumentCore();

        public void AddCustomerInfo(Customer customer)
        {
            DocumentBuilder db = new DocumentBuilder(dc);
            db.CharacterFormat.Size = 15.0f;
            db.CharacterFormat.FontColor = SautinSoft.Document.Color.Black;

            db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
            db.CharacterFormat.Bold = true;
            db.Write("Customer:");
            db.CharacterFormat.Bold = false;
            db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
            db.Write("FirstName: " + customer.FirstName);
            db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
            db.Write("LastName: " + customer.LastName);
            db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
        }

        public void AddPaymentMethodInfo(PaymentMethod paymentMethod)
        {
            DocumentBuilder db = new DocumentBuilder(dc);
            db.CharacterFormat.Size = 15.0f;
            db.CharacterFormat.FontColor = SautinSoft.Document.Color.Black;

            db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
            db.CharacterFormat.Bold = true;
            db.Write("PaymentMethod:");
            db.CharacterFormat.Bold = false;
            db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
            db.Write("Name: " + paymentMethod.Name);
            db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
        }

        public void AddProductsInfo(List<Product> products)
        {
            DocumentBuilder db = new DocumentBuilder(dc);
            db.CharacterFormat.Size = 15.0f;
            db.CharacterFormat.FontColor = SautinSoft.Document.Color.Black;

            db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
            db.CharacterFormat.Bold = true;
            db.Write("Ordered products:");
            db.CharacterFormat.Bold = false;
            db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);

            foreach (var product in products)
            {
                db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
                db.Write("Name: " + product.Name);
                db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
                db.Write("Price: " + product.Price);
                db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
                db.Write("VAT: " + product.VAT);
                db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
            }

            db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
        }

        public void AddSellerInfo()
        {
            DocumentBuilder db = new DocumentBuilder(dc);
            db.CharacterFormat.FontName = "Verdana";
            db.CharacterFormat.Size = 30.0f;
            db.CharacterFormat.FontColor = SautinSoft.Document.Color.Orange;
            db.CharacterFormat.Bold = true;
            db.Write("Invoice");
            db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
            db.CharacterFormat.Bold = false;

            db.CharacterFormat.Size = 15.0f;
            db.CharacterFormat.FontColor = SautinSoft.Document.Color.Black;

            db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
            db.CharacterFormat.Bold = true;
            db.Write("Seller Info:");
            db.CharacterFormat.Bold = false;
            db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
            db.Write("FirstName: Robert");
            db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
            db.Write("LastName: Lewandowski");
            db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
        }

        public void AddShippingMethodInfo(ShippingMethod shippingMethod)
        {
            DocumentBuilder db = new DocumentBuilder(dc);
            db.CharacterFormat.Size = 15.0f;
            db.CharacterFormat.FontColor = SautinSoft.Document.Color.Black;

            db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
            db.CharacterFormat.Bold = true;
            db.Write("ShippingMethod:");
            db.CharacterFormat.Bold = false;
            db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
            db.Write("Name: " + shippingMethod.Name);
            db.InsertSpecialCharacter(SpecialCharacterType.LineBreak);
        }

        public DocumentCore GetInvoiceInPdf()
        {
            return dc;
        }
    }
}