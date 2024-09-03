using CoffeeCounter.Data;
using CoffeeCounter.Model;
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
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            LoadCoffeeData();
        }

        public void LoadCoffeeData()
        {
            // This method should reload the coffee data from the database
            using (var dbContext = new CoffeeDbContext())
            {
                CoffeeList.ItemsSource = dbContext.Coffee.ToList();
            }
        }

        //private void AddCoffee_Click(object sender, RoutedEventArgs e)
        //{
        //    AddCoffee addCoffeeWindow = new AddCoffee();

        //    addCoffeeWindow.DataContext = new AddCoffeeViewModel();

        //    // Show the AddCoffee window
        //    addCoffeeWindow.Show();
        //}

        private void AddCoffee_Click(object sender, RoutedEventArgs e)
        {
            // Open the AddCoffee window and handle closing it
            AddCoffee addCoffeeWindow = new AddCoffee();
            addCoffeeWindow.Closed += (s, args) => LoadCoffeeData(); // Refresh data on window close
            addCoffeeWindow.Show();
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CoffeeList.Items.Filter = FilterMethod;
        }

        private bool FilterMethod(object obj)
        {
            var coffee = (Model.Coffee)obj;

            return coffee.Kind.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);


        }
    }
}
