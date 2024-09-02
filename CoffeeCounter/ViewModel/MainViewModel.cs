using CoffeeCounter.Model;
using CoffeeCounter.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UsersIndex.Commands;

namespace CoffeeCounter.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<Coffee> Coffee { get; set; }
        public ICommand ShowWindowCommand { get; set; }

        public MainViewModel()
        {
            Coffee = CoffeeManager.GetCoffee();
            ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);
        }

        private bool CanShowWindow(object obj)
        {
            return true;
        }

        private void ShowWindow(object obj)
        {
            var mainWindow = obj as Window;

            AddCoffee addCoffeeWin = new AddCoffee();
            addCoffeeWin.Owner = mainWindow;
            addCoffeeWin.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addCoffeeWin.Show();
        }
    }
}
