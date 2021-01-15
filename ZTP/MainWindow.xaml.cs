using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ZTP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Database database = Database.GetInstance();

        public IList<Product> productsList { get; set; } = new ObservableCollection<Product>();
        //public Product currentProduct = null;

        public IList<Customer> customersList { get; set; } = new ObservableCollection<Customer>();
        public IList<ShippingMethod> shippingMethodsList { get; set; } = new ObservableCollection<ShippingMethod>();
        public IList<PaymentMethod> paymentMethodsList { get; set; } = new ObservableCollection<PaymentMethod>();
        public IList<Order> ordersList { get; set; } = new ObservableCollection<Order>();
        public IList<ProductOrder> productOrdersList { get; set; } = new ObservableCollection<ProductOrder>();
        public IList<CustomerProduct> customerProductsList { get; set; } = new ObservableCollection<CustomerProduct>();


        //shoppingCartList
        //addedPackagesList
        //subscribedProductsList

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            productsList = new ObservableCollection<Product>(database.GetAllProducts().ToList());
            customersList = new ObservableCollection<Customer>(database.GetAllCustomers().ToList());
            shippingMethodsList = new ObservableCollection<ShippingMethod>(database.GetAllShippingMethods().ToList());
            paymentMethodsList = new ObservableCollection<PaymentMethod>(database.GetAllPaymentMethods().ToList());
            ordersList = new ObservableCollection<Order>(database.GetAllOrders().ToList());
            productOrdersList = new ObservableCollection<ProductOrder>(database.GetAllProductOrders().ToList());
            customerProductsList = new ObservableCollection<CustomerProduct>(database.GetAllCustomerProducts().ToList());
            comboBox_OrderStatus.ItemsSource = Enum.GetValues(typeof(State)).Cast<State>();


        }

        #region Product list

        private void MenuItem_AddProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_EditProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_DeleteProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_AddToCart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Observe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void listBox_ProductsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region Shopping cart

        private void MenuItem_AddBubbleWrap_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_AddCardboardBox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_AddPlasticBox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void listBox_ShoppingCartList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region Order

        private void button_Order_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Customer

        private void MenuItem_UnsubscribeProduct_Click(object sender, RoutedEventArgs e)
        {

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

        #endregion
    }
}
