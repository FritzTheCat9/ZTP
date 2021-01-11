using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP
{
    public class Database
    {
        /* POLA */

        private static Database instance = new Database();
        /*private ICollection<Product> Products;
        private ICollection<Category> Categories;
        private ICollection<Customer> Customers;
        private ICollection<Address> Addresses;
        private ICollection<ShippingMethod> ShippingMethods;
        private ICollection<PaymentMethod> PaymentMethods;
        private ICollection<Order> Orders;
        private ICollection<ProductOrder> ProductOrders;*/

        /* METODY */

        private Database() { }
        public static Database GetInstance()
        {
            if (instance == null)
            {
                instance = new Database();
            }
            return instance;
        }
    }
}
