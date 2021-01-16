using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Models;

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

        /*private void createCategories() { }
        private void createProducts() { }
        private void createUsers() { }
        private void createPayments() { }*/

        public Customer CheckLoginAndPassword(string login, string password)
        {
            using (var context = new DatabaseContext())
            {
                var user = context.Customers.Where(x => x.Login == login).Where(x => x.Password == password).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }

                return null;
            }
        }

        #region GetAll Methods

        public IEnumerable<Product> GetAllProducts()
        {
            using (var context = new DatabaseContext())
            {
                var products = context.Products.ToList();
                return products;
            }
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            using (var context = new DatabaseContext())
            {
                var customers= context.Customers.ToList();
                return customers;
            }
        }

        public IEnumerable<ShippingMethod> GetAllShippingMethods()
        {
            using (var context = new DatabaseContext())
            {
                var shippingMethods = context.ShippingMethods.ToList();
                return shippingMethods;
            }
        }

        public IEnumerable<PaymentMethod> GetAllPaymentMethods()
        {
            using (var context = new DatabaseContext())
            {
                var paymentMethods = context.PaymentMethods.ToList();
                return paymentMethods;
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            using (var context = new DatabaseContext())
            {
                var orders = context.Orders.Include(o => o.Customer).Include(o => o.ShippingMethod).Include(o => o.PaymentMethod).ToList();
                return orders;
            }
        }

        public IEnumerable<ProductOrder> GetAllProductOrders()
        {
            using (var context = new DatabaseContext())
            {
                var productOrders = context.ProductOrders.Include(po => po.Order).Include(po => po.Product).ToList();
                return productOrders;
            }
        }

        public IEnumerable<CustomerProduct> GetAllCustomerProducts()
        {
            using (var context = new DatabaseContext())
            {
                var customerProducts = context.CustomerProducts.Include(cp => cp.Customer).Include(cp => cp.Product).ToList();
                return customerProducts;
            }
        }
        #endregion

        public Order AddOrder(Order order)
        {
            using (var context = new DatabaseContext())
            {
                var newOrder = new Order { Customer = order.Customer, ShippingMethod = order.ShippingMethod, PaymentMethod = order.PaymentMethod, Price = order.Price, OrderStatus = order.OrderStatus };
                var customer = context.Customers.Find(newOrder.CustomerID);
                var shippingMethod = context.ShippingMethods.Find(newOrder.ShippingMethodID);
                var paymentMethod = context.PaymentMethods.Find(newOrder.PaymentMethodID);
                customer.Orders.Add(newOrder);
                shippingMethod.Orders.Add(newOrder);
                paymentMethod.Orders.Add(newOrder);
                context.SaveChanges();
                return newOrder;
            }
        }

        #region Products Methods

        public bool AddProduct(Product product)
        {
            using (var context = new DatabaseContext())
            {
                var newProduct = new Product { Name = product.Name, Promotion = product.Promotion, VAT = product.VAT, Price = product.Price, Quantity = product.Quantity };
                var entity = context.Products.Attach(product);
                entity.State = EntityState.Added;
                context.SaveChanges();
                return true;

                /*var newProduct = new Product { Name = product.Name, Promotion = product.Promotion, VAT = product.VAT, Price = product.Price, Quantity = product.Quantity };
                context.Products.Add(newProduct);
                context.SaveChanges();
                return true;*/
            }
        }

        public bool RemoveProduct(Product product)
        {
            using (var context = new DatabaseContext())
            {
                var entity = context.Products.Attach(product);
                entity.State = EntityState.Deleted;
                context.SaveChanges();
                return true;

                /*context.Products.Remove(product);
                context.SaveChanges();
                return true;*/
            }
        }

        public bool UpdateProduct(Product product)
        {
            using (var context = new DatabaseContext())
            {
                var entity = context.Products.Attach(product);
                entity.State = EntityState.Modified;
                context.SaveChanges();
                return true;

                /*context.Products.Update(product);
                context.SaveChanges();
                return true;*/
            }
        }

        #endregion
    }
}
