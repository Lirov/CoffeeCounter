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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
            LoginViewModel viewModel = new LoginViewModel();
            this.DataContext = viewModel;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CoffeeDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
                sqlConnection.Open();
                string query = "INSERT INTO Users (username, password) VALUES (@username, @password)";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);

                cmd.Parameters.AddWithValue("@username", UsernameTextBox.Text);
                cmd.Parameters.AddWithValue("@password", PasswordBox.Password);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User registered successfully");
                UsernameTextBox.Text = "";
                PasswordBox.Password = "";
                Main main = new Main();
                this.Close();
                main.Show();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void BackLink_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
            var loginWindow = new Login(); 
            loginWindow.Show();
            this.Close();
        }
    }
}
