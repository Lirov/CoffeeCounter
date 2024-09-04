using CoffeeCounter.Commands;
using CoffeeCounter.Data;
using CoffeeCounter.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CoffeeCounter.ViewModel
{
    class EditCoffeeViewModel: INotifyPropertyChanged
    {
        private readonly CoffeeDbContext _dbContext;
        private readonly Coffee _coffee;

        public ICommand SaveCommand { get; set; }

        private string _kind;
        public string Kind
        {
            get => _kind;
            set
            {
                _kind = value;
                OnPropertyChanged();
            }
        }

        private string _volume;
        public string Volume
        {
            get => _volume;
            set
            {
                _volume = value;
                OnPropertyChanged();
            }
        }

        private string _time;
        public string Time
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged();
            }
        }

        private string _date;
        public string Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        public EditCoffeeViewModel(CoffeeDbContext coffeeDbContext, Coffee coffee)
        {
            _dbContext = coffeeDbContext;
            _coffee = coffee;

            Kind = coffee.Kind;
            Volume = coffee.Volume;
            Time = coffee.Time;
            Date = coffee.Date;
            Location = coffee.Location;

            SaveCommand = new RelayCommand(Save,CanSave);
        }

        public EditCoffeeViewModel()
        {
        }

        private bool CanSave(object obj)
        {
            return !string.IsNullOrWhiteSpace(Kind) &&
           !string.IsNullOrWhiteSpace(Volume) &&
           !string.IsNullOrWhiteSpace(Time) &&
           !string.IsNullOrWhiteSpace(Date) &&
           !string.IsNullOrWhiteSpace(Location);
        }

        private void Save(object obj)
        {          
                _coffee.Kind = Kind;
                _coffee.Volume = Volume;
                _coffee.Time = Time;
                _coffee.Date = Date;
                _coffee.Location = Location;
                _dbContext.Update(_coffee);
                _dbContext.SaveChanges();

                var window = Application.Current.Windows.OfType<EditCoffee>().FirstOrDefault();
                window?.Close();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
