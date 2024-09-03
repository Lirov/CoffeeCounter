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
                // Ensure database connection string is correct and working
                using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CoffeeDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"))
                {
                    sqlConnection.Open();

                    // Prepare the SQL command
                    string query = "INSERT INTO Coffee (kind, volume, time, date, location) VALUES (@kind, @volume, @time, @date, @location)";
                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        // Add parameters with appropriate values
                        cmd.Parameters.AddWithValue("@kind", Kind.Text ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@volume", Volume.Text ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@time", Time.Text ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@date", Date.Text ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@location", Location.Text ?? (object)DBNull.Value);

                        // Execute the command
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Coffee added successfully.");

                // Clear the text fields
                Kind.Text = "";
                Volume.Text = "";
                Time.Text = "";
                Date.Text = "";

                // Close current window and open main window
                this.Close();
                Main main = new Main();
                main.Show();
            }
            catch (Exception ex)
            {
                // Log the exception and provide feedback to the user
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
