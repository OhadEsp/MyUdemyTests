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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InvoiceManagement
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void OnLoginBtnClicked(object sender, RoutedEventArgs e)
        {
            string passwordEntered = PasswordBox.Password;
            
            // In order to make it work - need to add the environment variable to the system variables and restart computer.
            string? envPw = Environment.GetEnvironmentVariable("InvoiceManagement");

            if (envPw != null)
            {
                MessageBox.Show(passwordEntered == envPw ? "Entered correct password." : "Entered wrong password.");
            }
            else
            {
                MessageBox.Show("Environment variable was not found.");
            }
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            LoginButton.IsEnabled = !string.IsNullOrEmpty(PasswordBox.Password);
        }
    }
}
