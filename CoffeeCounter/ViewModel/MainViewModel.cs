using CoffeeCounter.Commands;
using CoffeeCounter.Data;
using CoffeeCounter.Model;
using CoffeeCounter.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Coffee = CoffeeCounter.Model.Coffee;

namespace CoffeeCounter.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<Coffee> Coffee { get; set; }
        public ICommand ShowWindowCommand { get; set; }
        public ICommand NextPageCommand { get; set; }
        public ICommand PreviousPageCommand { get; set; }
        public ICommand OpenPagingWindowCommand { get; set; }

        public MainViewModel()
        {
            Coffee = CoffeeManager.GetCoffee();
            ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);
            OpenPagingWindowCommand = new RelayCommand(OpenPagingWindow);
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
        private void OpenPagingWindow(object obj)
        {
            Paging pagingWindow = new Paging();
            pagingWindow.DataContext = new PagingViewModel();
            pagingWindow.Show(); // Show the new paging window
        }


    }
}
