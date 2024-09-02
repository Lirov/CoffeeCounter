using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UsersIndex.Commands;

namespace CoffeeCounter.ViewModel
{
    public class LoginViewModel
    {
        public ICommand LoginCommand { get; set; }
        public ICommand RecoverPasswordCommand { get; set; }
        public ICommand ShowPasswordCommand { get; set; }
        public ICommand RememberPasswordCommand { get; set; }

        private string _username;
        private string _email;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        public string UserName { get => _username; set => _username = value; }
        public string Email { get => _email; set => _email = value;}
        public SecureString Password { get => _password; set => _password = value; }
        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }
        public bool IsViewVisible { get => _isViewVisible; set => _isViewVisible = value; }

        public LoginViewModel() {
            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new RelayCommand(p => ExecuteRecoverPassCommand("",""));
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(UserName) || UserName.Length < 3 || Password == null || Password.Length < 3)
                validData = false;
            else validData = true;
            return validData;
        }

        private async void ExecuteLoginCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                ErrorMessage = "Username cannot be empty.";
                return;
            }

            if (Password == null || Password.Length < 6)
            {
                ErrorMessage = "Password must be at least 6 characters long.";
                return;
            }

            ErrorMessage = string.Empty;

            bool isAuthenticated = await AuthenticateAsync(UserName, Password);

            if (isAuthenticated)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                Application.Current.MainWindow.Close();
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
            }
        }

        private async Task<bool> AuthenticateAsync(string username, SecureString password)
        {
            await Task.Delay(1000);

            return username == "testuser" && ConvertToUnsecureString(password) == "password123";
        }
        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
                return null;

            IntPtr bstr = IntPtr.Zero;
            try
            {
                bstr = Marshal.SecureStringToBSTR(securePassword);
                return Marshal.PtrToStringBSTR(bstr);
            }
            finally
            {
                if (bstr != IntPtr.Zero)
                    Marshal.FreeBSTR(bstr);
            }
        }

        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }
    }
}
