using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Models
{
    public class Admin
    {
        public List<Order> ShowCustomerOrders(Customer customer) { return null; }
        public bool ChangeOrderStatus(Order o) { return false; }
        public bool AddProduct(Product p) { return false; }
        public bool DeleteProduct(Product p) { return false; }
        public bool EditProduct(Product p) { return false; }
        public bool AddCategory(Category c) { return false; }
        public bool DeleteCategory(Category c) { return false; }
        public bool EditCategory(Category c) { return false; }
        public bool AddShippingMethod(ShippingMethod sm) { return false; }
        public bool DeleteShippingMethod(ShippingMethod sm) { return false; }
        public bool EditShippingMethod(ShippingMethod sm) { return false; }
        public bool AddPaymentMethod(PaymentMethod pm) { return false; }
        public bool DeletePaymentMethod(PaymentMethod pm) { return false; }
        public bool EditPaymentMethod(PaymentMethod pm) { return false; }
        public void InitShop() { }
    }
}
