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
        public List<Person> People = new List<Person>
        {
            new Person{Name = "Jannic", Age = 30},
            new Person{Name = "Marc", Age = 20},
            new Person{Name = "Maria", Age = 40},
            new Person{Name = "Scott", Age = 35},
            new Person{Name = "Lucas", Age = 27}
        };

        public MainWindow()
        {
            InitializeComponent();

            ListBoxNames.ItemsSource = People;
        }
    }
}
