using SautinSoft.Document;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZTP.Models;
using ZTP.Patterns;
using ZTP.Patterns.Iterator;

namespace ZTP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private Database database = Database.GetInstance();
        private Database database = null;
        public Customer customer { get; set; } = null;
        private decimal shoppingCartPrice { get; set; } = 0;

        /* LISTY Z BAZY DANYCH */
        public IList<Product> productsList { get; set; } = new ObservableCollection<Product>();
        public IList<Customer> customersList { get; set; } = new ObservableCollection<Customer>();
        public IList<ShippingMethod> shippingMethodsList { get; set; } = new ObservableCollection<ShippingMethod>();
        public IList<PaymentMethod> paymentMethodsList { get; set; } = new ObservableCollection<PaymentMethod>();
        public IList<Order> ordersList { get; set; } = new ObservableCollection<Order>();
        public IList<ProductOrder> productOrdersList { get; set; } = new ObservableCollection<ProductOrder>();
        public IList<CustomerProduct> customerProductsList { get; set; } = new ObservableCollection<CustomerProduct>();

        /* LISTY DO WIDOKU */
        public IList<Product> shoppingCartList { get; set; } = new ObservableCollection<Product>();
        public IList<Product> subscribedProductsList { get; set; } = new ObservableCollection<Product>();
        public IList<Product> productsInSelectedOrder { get; set; } = new ObservableCollection<Product>();

        /* DECORATOR - PATTERN */
        public IList<ProductDecorator> shoppingCartDecoratorsList { get; set; } = new ObservableCollection<ProductDecorator>();
        bool cartChanged = false;
        decimal packagesPrice = 0;

        /* BUILDER - PATTERN */
        private void constructInvoice(InvoiceBuilder invoiceBuilder, Order order, IList<ProductDecorator> shoppingCartDecoratorsList)
        {
            invoiceBuilder.AddSellerInfo();
            invoiceBuilder.AddCustomerInfo(order.Customer);
            invoiceBuilder.AddPrice(order.Price);
            invoiceBuilder.AddPaymentMethodInfo(order.PaymentMethod);
            invoiceBuilder.AddShippingMethodInfo(order.ShippingMethod);
            invoiceBuilder.AddProductsInfo(shoppingCartList.ToList(), shoppingCartDecoratorsList);
        }

        public MainWindow(LoginWindow loginWindow)
        {
            InitializeComponent();
            DataContext = this;

            customer = loginWindow.customer;
            database = loginWindow.database;

            productsList = new ObservableCollection<Product>(database.GetAllProducts().ToList());
            customersList = new ObservableCollection<Customer>(database.GetAllCustomers().ToList());
            shippingMethodsList = new ObservableCollection<ShippingMethod>(database.GetAllShippingMethods().ToList());
            paymentMethodsList = new ObservableCollection<PaymentMethod>(database.GetAllPaymentMethods().ToList());
            ordersList = new ObservableCollection<Order>(database.GetAllOrders().ToList());
            productOrdersList = new ObservableCollection<ProductOrder>(database.GetAllProductOrders().ToList());
            customerProductsList = new ObservableCollection<CustomerProduct>(database.GetAllCustomerProducts().ToList());
            comboBox_OrderStatus.ItemsSource = Enum.GetValues(typeof(State)).Cast<State>();
            shoppingCartList = new ObservableCollection<Product>();                                                             /* OBSERVER */
            subscribedProductsList = new ObservableCollection<Product>(database.GetAllCustomerProducts(customer));              /* OBSERVER */
            shoppingCartDecoratorsList = new ObservableCollection<ProductDecorator>();                                          /* DECORATOR */

            DisableComponents(customer);
        }

        void DisableComponents(Customer customer)
        {
            if (customer.AdminRights == false)
            {
                if(MenuItem_AddProduct != null &&
                    MenuItem_DeleteProduct != null &&
                    TextBox_Name != null &&
                    TextBox_VAT != null &&
                    TextBox_Price != null &&
                    TextBox_Quantity != null &&
                    CheckBox_Promotion != null &&
                    TabItem_Admin != null)
                {
                    MenuItem_AddProduct.Visibility = Visibility.Hidden;
                    MenuItem_DeleteProduct.Visibility = Visibility.Hidden;
                    TextBox_Name.IsEnabled = false;
                    TextBox_VAT.IsEnabled = false;
                    TextBox_Price.IsEnabled = false;
                    TextBox_Quantity.IsEnabled = false;
                    CheckBox_Promotion.IsEnabled = false;
                    TabItem_Admin.Visibility = Visibility.Hidden;
                }
            }
        }

        #region Product list

        private void MenuItem_AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = new Product { Name = "New", Price = 0, Promotion = false, Quantity = 0, VAT = 23 };
            
            if(database.AddProduct(product))
            {
                productsList.Add(product);
                listBox_ProductsList.SelectedIndex = productsList.Count;
            }
        }

        private void MenuItem_DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_ProductsList.SelectedIndex >= 0)
            {
                var product = (Product)listBox_ProductsList.SelectedItem;
                
                for(int i = shoppingCartList.Count - 1; i >= 0; i--)                // usunięcie produktu z shopping cart
                {
                    if (shoppingCartList[i].ProductID == product.ProductID)
                    {
                        shoppingCartList.Remove(shoppingCartList[i]);
                    }
                }

                cartChanged = true;

                for (int i = subscribedProductsList.Count - 1; i >= 0; i--)                // usunięcie produktu z produktów obserwowanych
                {
                    if (subscribedProductsList[i].ProductID == product.ProductID)
                    {
                        subscribedProductsList.Remove(subscribedProductsList[i]);
                    }
                }

                if (database.RemoveProduct(product))
                {
                    productsList.Remove(product);
                }
            }
        }

        private void MenuItem_AddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_ProductsList.SelectedIndex >= 0)
            {
                var product = (Product)listBox_ProductsList.SelectedItem;
                if(product.Quantity > 0)
                {
                    var productsCount = shoppingCartList.Where(p => p.ProductID == product.ProductID).Count();

                    if(product.Quantity > productsCount)
                    {
                        shoppingCartList.Add(product);
                        shoppingCartPrice += product.Price;
                        cartChanged = true;
                    }
                }
            }
        }

        private void TextBox_UpdateProduct(object sender, RoutedEventArgs e)
        {
            if (listBox_ProductsList.SelectedIndex >= 0)
            {
                var product = (Product)listBox_ProductsList.SelectedItem;       

                database.UpdateProduct(product);
            }
        }

        private void TextBox_UpdateProduct(object sender, TextChangedEventArgs e)
        {
            if (listBox_ProductsList.SelectedIndex >= 0)
            {
                var product = (Product)listBox_ProductsList.SelectedItem;

                Brush color = Brushes.Black;

                if (product.Quantity > 0)
                {
                    color = Brushes.Green;
                }

                var i = -1;
                foreach (var subProduct in subscribedProductsList)
                {
                    i++;
                    if (subProduct.ProductID == product.ProductID)
                    {
                        ListBoxItem listElement = (ListBoxItem)listBox_SubscribedProductsList.ItemContainerGenerator.ContainerFromIndex(i);
                        if (listElement != null)
                        {
                            listElement.Foreground = color;
                        }
                    }
                }

                product.Notify();

                database.UpdateProduct(product);
            }
        }

        private void CheckObservers()
        {
            
        }

        private void MenuItem_Observe_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_ProductsList.SelectedIndex >= 0)
            {
                var product = (Product)listBox_ProductsList.SelectedItem;
                var customerProduct = new CustomerProduct { Customer = customer, Product = product };

                var allCustomerProducts = database.GetAllCustomerProducts(customer);

                bool isInList = false;
                foreach (var p in allCustomerProducts)
                {
                    if(p.ProductID == product.ProductID)
                    {
                        isInList = true;
                    }
                }

                if(!isInList && product.Quantity <= 0)
                {
                    if (database.AddCustomerProduct(customerProduct))
                    {                        
                        subscribedProductsList.Add(product);
                        
                        product.Attach(customer);
                    }
                }
            }
        }

        private void listBox_ProductsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region Shopping cart

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            if(cartChanged && TextBlock_Description != null && TextBlock_Description2 != null)
            {
                cartChanged = false;
                packagesPrice = 0;
                shoppingCartDecoratorsList.Clear();
                for (int i = 0; i < shoppingCartList.Count; i++)
                {
                    shoppingCartDecoratorsList.Add(new EmptyDecorator(shoppingCartList[i]));
                }
                TextBlock_Description.Text = "";
                TextBlock_Description2.Text = "";
            }
        }

        private void MenuItem_AddBubbleWrap_Click(object sender, RoutedEventArgs e)
        {
            var index = listBox_ShoppingCartList.SelectedIndex;
            if (index >= 0)
            {
                shoppingCartDecoratorsList[index] = new BubbleWrap(shoppingCartDecoratorsList[index]);
                TextBlock_Description.Text = shoppingCartDecoratorsList[index].getDescription();
                TextBlock_Description2.Text = shoppingCartDecoratorsList[index].getDescription();
                packagesPrice += 5;
            }
        }

        private void MenuItem_AddCardboardBox_Click(object sender, RoutedEventArgs e)
        {
            var index = listBox_ShoppingCartList.SelectedIndex;
            if (index >= 0)
            {
                shoppingCartDecoratorsList[index] = new CardboardBox(shoppingCartDecoratorsList[index]);
                TextBlock_Description.Text = shoppingCartDecoratorsList[index].getDescription();
                TextBlock_Description2.Text = shoppingCartDecoratorsList[index].getDescription();
                packagesPrice += 10;
            }
        }

        private void MenuItem_AddPlasticBox_Click(object sender, RoutedEventArgs e)
        {
            var index = listBox_ShoppingCartList.SelectedIndex;
            if (index >= 0)
            {
                shoppingCartDecoratorsList[index] = new PlasticBox(shoppingCartDecoratorsList[index]);
                TextBlock_Description.Text = shoppingCartDecoratorsList[index].getDescription();
                TextBlock_Description2.Text = shoppingCartDecoratorsList[index].getDescription();
                packagesPrice += 15;
            }
        }

        private void MenuItem_RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_ShoppingCartList.SelectedIndex >= 0)
            {
                var product = (Product)listBox_ShoppingCartList.SelectedItem;
                shoppingCartList.Remove(product);
                shoppingCartPrice -= product.Price;
                cartChanged = true;
            }
        }

        private void listBox_ShoppingCartList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = listBox_ShoppingCartList.SelectedIndex;
            if (index >= 0)
            {
                TextBlock_Description.Text = shoppingCartDecoratorsList[index].getDescription();
            }
        }

        #endregion

        #region Order

        private void listBox_OrderProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = listBox_OrderProductList.SelectedIndex;
            if (index >= 0)
            {
                TextBlock_Description2.Text = shoppingCartDecoratorsList[index].getDescription();
            }
        }

        private void button_Order_Click(object sender, RoutedEventArgs e)
        {
            if(shoppingCartList.Count != 0)
            {
                var shippingMethod = (ShippingMethod)comboBox_ShippingMethod.SelectedItem;
                var paymentMethod = (PaymentMethod)comboBox_PaymentMethod.SelectedItem;
                var order = new Order { Customer = customer, ShippingMethod = shippingMethod, PaymentMethod = paymentMethod, OrderStatus = State.Preparing, Price = shoppingCartPrice + packagesPrice };
                database.AddOrder(order);

                ordersList.Add(order);

                foreach (var product in shoppingCartList)
                {
                    var newProductOrder = new ProductOrder { Order = order, Product = product };
                    database.AddProductOrder(newProductOrder);
                    productOrdersList.Add(newProductOrder);
                }

                string workingDirectory = Environment.CurrentDirectory;
                string solutionDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                string invoicesDirectory = solutionDirectory + @"\Invoices\";
                var path = invoicesDirectory + order.OrderID + "_" + order.Customer.FirstName + "_" + order.Customer.LastName;

                if (comboBox_MakeInvoice.SelectedIndex == 0)
                {
                    InvoiceBuilderTxt invoiceBuilder = new InvoiceBuilderTxt();
                    constructInvoice(invoiceBuilder, order, shoppingCartDecoratorsList);
                    var invoiceText = invoiceBuilder.GetInvoiceInTxt();
                    path += ".txt";
                    File.WriteAllText(path, invoiceText);
                }
                else
                {
                    InvoiceBuilderPdf invoiceBuilder = new InvoiceBuilderPdf();
                    constructInvoice(invoiceBuilder, order, shoppingCartDecoratorsList);
                    DocumentCore invoicePdf = invoiceBuilder.GetInvoiceInPdf();
                    path += ".pdf";
                    invoicePdf.Save(path);

                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(path) { UseShellExecute = true });
                }

                for (int i = shoppingCartList.Count - 1; i >= 0; i--)                // usunięcie produktu z shopping cart
                {
                    var p = shoppingCartList[i];
                    p.Quantity--;
                    database.UpdateProduct(p);

                    shoppingCartList.Remove(shoppingCartList[i]);
                }

                shoppingCartPrice = 0;
                cartChanged = true;
            }

            
        }

        private void comboBox_MakeInvoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        #endregion

        #region Customer


        private void MenuItem_UnsubscribeProduct_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_SubscribedProductsList.SelectedIndex >= 0)
            {
                var product = (Product)listBox_SubscribedProductsList.SelectedItem;
                var customerProduct = new CustomerProduct { Customer = customer, Product = product };

                if (database.RemoveCustomerProduct(customerProduct))
                {
                    subscribedProductsList.Remove(product);
                    product.Detach(customer);
                }
            }
        }

      
        private void listBox_SubscribedProductsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region Admin

        private void listBox_OrdersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listBox_OrdersList.SelectedIndex >= 0)
            {
                var selectedOrder = (Order)listBox_OrdersList.SelectedItem;

                productsInSelectedOrder.Clear();

                foreach (var po in productOrdersList)
                {
                    if (po.OrderID == selectedOrder.OrderID)
                    {
                        productsInSelectedOrder.Add(po.Product);
                    }
                }
            }
        }

        private void button_discountProducts_Click(object sender, RoutedEventArgs e)
        {
            //changing price for even 5-th product
            var collection = new ProductsCollection(productsList, 5);         

            foreach (var element in collection)
            {
               //changing price here                
               var product = (Product)element;
               product.Price = Math.Round(product.Price * 95 / 100, 2);               
               database.UpdateProduct(product);
            }
        }

        private void comboBox_OrderStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox_OrdersList.SelectedIndex >= 0)
            {
                var order = (Order)listBox_OrdersList.SelectedItem;

                database.UpdateOrder(order);
            }
        }

        #endregion

       
    }
}
