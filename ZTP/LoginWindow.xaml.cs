using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ZTP
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            if (Database.GetInstance().CheckLoginAndPassword(TextBox_Login.Text, TextBox_Password.Text) != null)
            {
                Console.WriteLine("SIEMA");
                MainWindow mainWindow= new MainWindow();
                Close();
                mainWindow.ShowDialog();
            }

            /*Address address = new Address { Town="asd", Street="asd", PostCode="asd", Country="asd", HouseNumber=123 };
            Customer customer = new Customer { FirstName = "FirstName", LastName = "LastName", Login = "Login", Password = "Password", AdminRights = true, Address = address };
            Database.GetInstance().AddCustomer(customer);*/
        }
    }
}
