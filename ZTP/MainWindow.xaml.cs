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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZTP.Models;
using ZTP.Patterns;

namespace ZTP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Database database = Database.GetInstance();
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

        /* DECORATOR - PATTERN */
        public IList<ProductBase> addedPackagesList { get; set; } = new ObservableCollection<ProductBase>();
        Product changedProduct { get; set; } = null;
        BubbleWrap productInBubbleWrap { get; set; } = null;
        CardboardBox productInCardboardBox { get; set; } = null;
        PlasticBox productInPlasticBox { get; set; } = null;

        /* BUILDER - PATTERN */
        private void constructInvoice(InvoiceBuilder invoiceBuilder, Order order)
        {
            invoiceBuilder.AddSellerInfo();
            invoiceBuilder.AddCustomerInfo(order.Customer);
            invoiceBuilder.AddPaymentMethodInfo(order.PaymentMethod);
            invoiceBuilder.AddShippingMethodInfo(order.ShippingMethod);
            invoiceBuilder.AddProductsInfo(shoppingCartList.ToList());
        }

        public MainWindow(LoginWindow loginWindow)
        {
            InitializeComponent();
            DataContext = this;

            customer = loginWindow.customer;

            productsList = new ObservableCollection<Product>(database.GetAllProducts().ToList());
            customersList = new ObservableCollection<Customer>(database.GetAllCustomers().ToList());
            shippingMethodsList = new ObservableCollection<ShippingMethod>(database.GetAllShippingMethods().ToList());
            paymentMethodsList = new ObservableCollection<PaymentMethod>(database.GetAllPaymentMethods().ToList());
            ordersList = new ObservableCollection<Order>(database.GetAllOrders().ToList());
            productOrdersList = new ObservableCollection<ProductOrder>(database.GetAllProductOrders().ToList());
            customerProductsList = new ObservableCollection<CustomerProduct>(database.GetAllCustomerProducts().ToList());
            comboBox_OrderStatus.ItemsSource = Enum.GetValues(typeof(State)).Cast<State>();
            shoppingCartList = new ObservableCollection<Product>();
            subscribedProductsList = new ObservableCollection<Product>(database.GetAllCustomerProducts(customer));
            addedPackagesList = new ObservableCollection<ProductBase>();

            //addedPackagesList - do dodania potem
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

                if(!isInList)
                {
                    if (database.AddCustomerProduct(customerProduct))
                    {
                        subscribedProductsList.Add(product);
                    }
                }
            }
        }

        private void listBox_ProductsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region Shopping cart

        private void MenuItem_AddBubbleWrap_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_ShoppingCartList.SelectedIndex >= 0)
            {
                //var product = (Product)listBox_ShoppingCartList.SelectedItem;

                if(productInBubbleWrap == null)
                {
                    changedProduct = (Product)listBox_ShoppingCartList.SelectedItem;
                    productInBubbleWrap = new BubbleWrap(changedProduct);
                }
                else
                {
                    productInBubbleWrap = new BubbleWrap(productInBubbleWrap);
                }

                TextBlock_Description.Text = productInBubbleWrap.getDescription();

                addedPackagesList.Add(productInBubbleWrap);
            }
        }

        private void MenuItem_AddCardboardBox_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_ShoppingCartList.SelectedIndex >= 0)
            {
                //var product = (Product)listBox_ShoppingCartList.SelectedItem;

                if (productInCardboardBox == null)
                {
                    changedProduct = (Product)listBox_ShoppingCartList.SelectedItem;
                    productInCardboardBox = new CardboardBox(changedProduct);

                    var prod = new PlasticBox(new CardboardBox(changedProduct));
                    TextBlock_Description.Text = prod.getDescription();
                }
                else
                {
                    productInCardboardBox = new CardboardBox(productInCardboardBox);
                }

                //TextBlock_Description.Text = productInCardboardBox.getDescription();

                addedPackagesList.Add(productInCardboardBox);
            }

            /*if (listBox_ShoppingCartList.SelectedIndex >= 0)
            {
                var product = (Product)listBox_ShoppingCartList.SelectedItem;

                CardboardBox productInCardboardBox = new CardboardBox(product);

                TextBlock_Description.Text = productInCardboardBox.getDescription();

                addedPackagesList.Add(productInCardboardBox);
            }*/
        }

        private void MenuItem_AddPlasticBox_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_ShoppingCartList.SelectedIndex >= 0)
            {
                //var product = (Product)listBox_ShoppingCartList.SelectedItem;

                if (productInPlasticBox == null)
                {
                    changedProduct = (Product)listBox_ShoppingCartList.SelectedItem;
                    productInPlasticBox = new PlasticBox(changedProduct);
                }
                else
                {
                    productInPlasticBox = new PlasticBox(productInPlasticBox);
                }

                TextBlock_Description.Text = productInPlasticBox.getDescription();

                addedPackagesList.Add(productInPlasticBox);
            }

            /*if (listBox_ShoppingCartList.SelectedIndex >= 0)
            {
                var product = (Product)listBox_ShoppingCartList.SelectedItem;

                PlasticBox productInPlasticBox = new PlasticBox(product);

                TextBlock_Description.Text = productInPlasticBox.getDescription();

                addedPackagesList.Add(productInPlasticBox);
            }*/
        }

        private void MenuItem_RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_ShoppingCartList.SelectedIndex >= 0)
            {
                var product = (Product)listBox_ShoppingCartList.SelectedItem;
                shoppingCartList.Remove(product);

                shoppingCartPrice -= product.Price;
            }
        }

        private void listBox_ShoppingCartList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region Order

        private void button_Order_Click(object sender, RoutedEventArgs e)
        {
            var shippingMethod = (ShippingMethod)comboBox_ShippingMethod.SelectedItem;
            var paymentMethod = (PaymentMethod)comboBox_PaymentMethod.SelectedItem;
            var order = new Order { Customer = customer, ShippingMethod = shippingMethod, PaymentMethod = paymentMethod, OrderStatus = State.Preparing, Price = shoppingCartPrice };
            database.AddOrder(order);

            ordersList.Add(order);

            foreach (var product in shoppingCartList)
            {
                var newProductOrder = new ProductOrder { Order = order, Product = product };
                database.AddProductOrder(newProductOrder);
            }

            string workingDirectory = Environment.CurrentDirectory;
            string solutionDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string invoicesDirectory = solutionDirectory + @"\Invoices\";
            var path = invoicesDirectory + order.OrderID + "_" + order.Customer.FirstName + "_" + order.Customer.LastName;

            if (comboBox_MakeInvoice.SelectedIndex == 0)
            {
                InvoiceBuilderTxt invoiceBuilder = new InvoiceBuilderTxt();
                constructInvoice(invoiceBuilder, order);
                var invoiceText = invoiceBuilder.GetInvoiceInTxt();
                path += ".txt";
                File.WriteAllText(path, invoiceText);
            }
            else
            {
                InvoiceBuilderPdf invoiceBuilder = new InvoiceBuilderPdf();
                constructInvoice(invoiceBuilder, order);
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

        }

        private void button_discountProducts_Click(object sender, RoutedEventArgs e)
        {

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
