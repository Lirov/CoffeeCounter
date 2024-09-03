using CoffeeCounter.Data;
using CoffeeCounter.ViewModel;
using Microsoft.Data.SqlClient;
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
    /// Interaction logic for AddCoffee.xaml
    /// </summary>
    public partial class AddCoffee : Window
    {
        public AddCoffee()
        {
            InitializeComponent();
            AddCoffeeViewModel viewModel = new AddCoffeeViewModel();
            this.DataContext = viewModel;
        }

        private void AddCoffee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var viewModel = this.DataContext as AddCoffeeViewModel;

                if (viewModel != null)
                {
                    var newCoffee = new Coffee
                    {
                        Kind = viewModel.Kind,
                        Volume = viewModel.Volume,
                        Time = viewModel.Time,
                        Date = viewModel.Date,
                        Location = viewModel.Location
                    };

                    using (var dbContext = new CoffeeDbContext())
                    {
                        dbContext.Coffee.Add(newCoffee);
                        dbContext.SaveChanges();
                    }

                    MessageBox.Show("Coffee added successfully.");

                    viewModel.Kind = "";
                    viewModel.Volume = "";
                    viewModel.Time = "";
                    viewModel.Date = "";
                    viewModel.Location = "";

                    this.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
