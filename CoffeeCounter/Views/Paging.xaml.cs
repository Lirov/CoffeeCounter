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
    /// Interaction logic for Paging.xaml
    /// </summary>
    public partial class Paging : Window
    {
        public Paging()
        {
            InitializeComponent();
            this.DataContext = new PagingViewModel();
        }
    }
}
