using CoffeeCounter.ViewModel;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public static string LoggedInUserName { get; private set; }
        public Login()
        {
            InitializeComponent();
            LoginViewModel viewModel = new LoginViewModel();
            this.DataContext = viewModel;

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=localhost;Database=CoffeeDB;User=root;Password=;";

            //SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CoffeeDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
            {
                try
                {

                    if (mySqlConnection.State == ConnectionState.Closed)
                    {
                        mySqlConnection.Open();
                        String query = "SELECT COUNT(1) FROM Users WHERE username=@username AND password=@password";
                        MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@username", UsernameTextBox.Text);
                        cmd.Parameters.AddWithValue("@password", PasswordBox.Password);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count == 1)
                        {
                            LoggedInUserName = UsernameTextBox.Text;
                            Main mainWindow = new Main();
                            mainWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Username or Password is incorrect");
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }

    

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            this.Close();
            register.Show();

        }
        private string GetUserName()
        { 
            return LoggedInUserName;
        }
        private void UsernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text == "Username")
            {
                UsernameTextBox.Text = "";
                UsernameTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void UsernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                UsernameTextBox.Text = "Username";
                UsernameTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}
