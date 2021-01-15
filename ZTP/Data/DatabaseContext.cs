using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP
{
    class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        //public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        //public DbSet<Address> Addresses { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ZTPDatabase;Trusted_Connection=True;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShippingMethod>().HasData(
                new ShippingMethod() { ShippingMethodID = 1, Name = "Kurier UPS" },
                new ShippingMethod() { ShippingMethodID = 2, Name = "Kurier DPD" },
                new ShippingMethod() { ShippingMethodID = 3, Name = "Poczta Polska" });

            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod() { PaymentMethodID = 1, Name = "Szybki przelew" },
                new PaymentMethod() { PaymentMethodID = 2, Name = "Przelew" },
                new PaymentMethod() { PaymentMethodID = 3, Name = "Karta" });

            /*modelBuilder.Entity<Address>().HasData(
                new Address() { AddressID = 1, CustomerID = 1, Country = "Polska", Town = "Białystok", PostCode = "12-123", Street = "Miła", HouseNumber = 12 },
                new Address() { AddressID = 2, CustomerID = 2, Country = "Polska", Town = "Łomża", PostCode = "23-543", Street = "Piękna", HouseNumber = 32 },
                new Address() { AddressID = 3, CustomerID = 3, Country = "Polska", Town = "Gdańsk", PostCode = "56-765", Street = "Bursztynowa", HouseNumber = 43 });*/

            modelBuilder.Entity<Customer>().HasData(
                new Customer() { CustomerID = 1, /*AddressID = 1,*/ FirstName = "Jan", LastName = "Kowalski", Login = "kowalski", Password = "kowalski", AdminRights = true },
                new Customer() { CustomerID = 2, /*AddressID = 2,*/ FirstName = "Robert", LastName = "Nowak", Login = "nowak", Password = "nowak", AdminRights = false },
                new Customer() { CustomerID = 3, /*AddressID = 3,*/ FirstName = "Krzysztof", LastName = "Piekarski", Login = "piekarski", Password = "piekarski", AdminRights = false });

            /*modelBuilder.Entity<Category>().HasData(
               new Category() { CategoryID = 1, Name = "Laptopy" },
               new Category() { CategoryID = 2, Name = "Smartfony" });*/

            modelBuilder.Entity<Product>().HasData(
                new Product() { ProductID = 1, /*CategoryID = 1,*/ Name = "Laptop LENOVO", /*Description = "Dobry laptop LENOVO",*/ Image = "~/Images/Laptop LENOVO.jpg", /*DateAdded = new DateTime(2018, 3, 20), Promotion = false,*/ VAT = 23, Price = 4300, Quantity = 20 },
                new Product() { ProductID = 2, /*CategoryID = 1,*/ Name = "Laptop HUAWEI", /*Description = "Dobry laptop HUAWEI",*/ Image = "~/Images/Laptop HUAWEI.png", /*DateAdded = new DateTime(2019, 10, 13), Promotion = false,*/ VAT = 23, Price = 5000, Quantity = 59 },
                new Product() { ProductID = 3, /*CategoryID = 2,*/ Name = "Smartfon HUAWEI P30", /*Description = "Dobry smartfon HUAWEI P30",*/ Image = "~/Images/Smartfon HUAWEI P30.jpg", /*DateAdded = new DateTime(2017, 9, 9), Promotion = true,*/ VAT = 23, Price = 2999, Quantity = 67 });

            modelBuilder.Entity<Order>().HasData(
                new Order() { OrderID = 1, CustomerID = 1, ShippingMethodID = 1, PaymentMethodID = 3, OrderStatus = State.Preparing, Price = 4300 },
                new Order() { OrderID = 2, CustomerID = 2, ShippingMethodID = 2, PaymentMethodID = 2, OrderStatus = State.OnTheWay, Price = 5000 },
                new Order() { OrderID = 3, CustomerID = 3, ShippingMethodID = 2, PaymentMethodID = 1, OrderStatus = State.OnTheWay, Price = 2999 },
                new Order() { OrderID = 4, CustomerID = 2, ShippingMethodID = 3, PaymentMethodID = 1, OrderStatus = State.Delivered, Price = 2999 },
                new Order() { OrderID = 5, CustomerID = 3, ShippingMethodID = 3, PaymentMethodID = 3, OrderStatus = State.Delivered, Price = 9300 });

            modelBuilder.Entity<ProductOrder>().HasData(
               new ProductOrder() { ProductOrderID = 1, ProductID = 1, OrderID = 1 },
               new ProductOrder() { ProductOrderID = 2, ProductID = 2, OrderID = 2 },
               new ProductOrder() { ProductOrderID = 3, ProductID = 3, OrderID = 3 },
               new ProductOrder() { ProductOrderID = 4, ProductID = 3, OrderID = 4 },
               new ProductOrder() { ProductOrderID = 5, ProductID = 1, OrderID = 5 },
               new ProductOrder() { ProductOrderID = 6, ProductID = 2, OrderID = 5 });

            base.OnModelCreating(modelBuilder);
        }
    }
}
