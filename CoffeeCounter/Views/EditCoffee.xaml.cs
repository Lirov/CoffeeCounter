using CoffeeCounter.Data;
using CoffeeCounter.ViewModel;
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

namespace CoffeeCounter.Views
{
    /// <summary>
    /// Interaction logic for EditCoffee.xaml
    /// </summary>
    public partial class EditCoffee : Window
    {
        public EditCoffee()
        {
            InitializeComponent();
            EditCoffeeViewModel viewModel = new EditCoffeeViewModel();
            this.DataContext = viewModel;

        }
    }
}
