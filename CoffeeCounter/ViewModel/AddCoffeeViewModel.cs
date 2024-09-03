using CoffeeCounter.Commands;
using CoffeeCounter.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CoffeeCounter.ViewModel
{
    public class AddCoffeeViewModel: INotifyPropertyChanged
    {
        private bool _isViewVisible = true;

        public ICommand AddCoffeeCommand { get; set; }
        public string? Kind { get; set; }
        public string? Volume { get; set; }
        public string? Time { get; set; }
        public string? Date { get; set; }
        public string? Location { get; set; }

        public bool IsViewVisible
        {
            get => _isViewVisible;
            set
            {
                if (_isViewVisible != value)
                {
                    _isViewVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public AddCoffeeViewModel() {

            AddCoffeeCommand = new RelayCommand(AddCoffee, CanAddCoffee);
        }

        private bool CanAddCoffee(object obj)
        {
            return true;
        }

        private void AddCoffee(object obj)
        {
           CoffeeManager.AddCoffee(new Coffee() { Kind = Kind, Volume = Volume, Time = Time, Date = Date, Location = Location});
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
