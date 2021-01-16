using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public IEnumerable<Product> GetAllCustomerProducts(Customer customer)
        {
            using (var context = new DatabaseContext())
            {
                IList<Product> productList = new ObservableCollection<Product>();
                var customerProducts = context.CustomerProducts.Include(cp => cp.Customer).Include(cp => cp.Product).Where(cp => cp.Customer.CustomerID == customer.CustomerID).ToList();

                foreach (var cp in customerProducts)
                {
                    productList.Add(cp.Product);
                }

                return productList;
            }
        }

        #endregion

        #region Order Methods

        public Order AddOrder(Order order)
        {
            using (var context = new DatabaseContext())
            {
                var entity = context.Orders.Attach(order);
                entity.State = EntityState.Added;

                context.SaveChanges();
                return order;
            }
        }

        public ProductOrder AddProductOrder(ProductOrder productOrder)
        {
            using (var context = new DatabaseContext())
            {
                var entity = context.ProductOrders.Attach(productOrder);
                entity.State = EntityState.Added;

                context.SaveChanges();
                return productOrder;
            }
        }

        #endregion

        #region Products Methods

        public bool AddProduct(Product product)
        {
            using (var context = new DatabaseContext())
            {
                var entity = context.Products.Attach(product);
                entity.State = EntityState.Added;
                context.SaveChanges();
                return true;
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
            }
        }

        public bool AddCustomerProduct(CustomerProduct customerProduct)
        {
            using (var context = new DatabaseContext())
            {
                var entity = context.CustomerProducts.Attach(customerProduct);
                entity.State = EntityState.Added;

                context.SaveChanges();
                return true;
            }
        }

        public bool RemoveCustomerProduct(CustomerProduct customerProduct)
        {
            using (var context = new DatabaseContext())
            {
                var customer = customerProduct.Customer;
                var product = customerProduct.Product;
                var customerProductToRemove = context.CustomerProducts.Where(cp => cp.CustomerID == customer.CustomerID && cp.ProductID == product.ProductID).FirstOrDefault();

                if(customerProductToRemove != null)
                {
                    context.Remove(customerProductToRemove);
                }

                context.SaveChanges();
                return true;
            }
        }

        #endregion
    }
}
