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

namespace WpfDataBindingDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // In one way binding we bind from source to our target. The target are the UI elements.
        Person person = new Person
        {
            Name = "Ohad",
            Age = 36
        };

        public MainWindow()
        {
            InitializeComponent();

            // In WPF it's the underlying data structure that provides the binding information between the UI elements and the data they display.
            this.DataContext = person;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello World");
        }
    }
}
