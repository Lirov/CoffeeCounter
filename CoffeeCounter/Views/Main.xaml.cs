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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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

            UserNameTextBlock.Text = $"{Login.LoggedInUserName}";

        }
        public void LoadCoffeeData()
        {
            // Reload the coffee data from the database
            using (var dbContext = new CoffeeDbContext())
            {
                CoffeeList.ItemsSource = dbContext.Coffee.ToList();
            }
        }
        private void AddCoffee_Click(object sender, RoutedEventArgs e)
        {
            AddCoffee addCoffeeWindow = new AddCoffee();
            addCoffeeWindow.Closed += (s, args) => LoadCoffeeData();
            addCoffeeWindow.Show();
        }
        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CoffeeList.Items.Filter = FilterMethod;
        }
        private void LogOff_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new Login();
            loginWindow.Show();
            this.Close();

        }
        private bool FilterMethod(object obj)
        {
            var coffee = (Coffee)obj;

            return coffee.Kind.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }
        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedCoffee = CoffeeList.SelectedItem as Coffee;

            if (selectedCoffee != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    using (var dbContext = new CoffeeDbContext())
                    {
                        var coffeeToRemove = dbContext.Coffee.Find(selectedCoffee.Id);
                        if (coffeeToRemove != null)
                        {
                            dbContext.Coffee.Remove(coffeeToRemove);
                            dbContext.SaveChanges();
                        }
                    }
                    LoadCoffeeData();
                }
            }
        }
        private void CoffeeList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedCoffee = CoffeeList.SelectedItem as Coffee;
            if (selectedCoffee != null)
            {
                EditCoffee editWindow = new EditCoffee();
                var viewModel = new EditCoffeeViewModel(new CoffeeDbContext(),selectedCoffee);
                editWindow.DataContext = viewModel;
                editWindow.ShowDialog(); // Use ShowDialog() to open the window as a modal
                LoadCoffeeData();
            }
        }

        private void PagingButton_Click(object sender, RoutedEventArgs e)
        {
            Paging pagingWindow = new Paging();
            pagingWindow.DataContext = new PagingViewModel();
            pagingWindow.Show();
        }

    }
}
