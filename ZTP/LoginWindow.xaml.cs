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
using ZTP.Models;

namespace ZTP
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public Customer customer = null;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            customer = Database.GetInstance().CheckLoginAndPassword(TextBox_Login.Text, TextBox_Password.Text);

            if (customer != null)
            {
                MainWindow mainWindow= new MainWindow(this);
                Close();
                mainWindow.ShowDialog();
            }
        }
    }
}
