using CoffeeCounter.Commands;
using CoffeeCounter.Data;
using CoffeeCounter.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CoffeeCounter.ViewModel
{
    public class PagingViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<Coffee> CoffeeRecords { get; set; }

        private int _currentMonth = 1;
        private const int PageSize = 10;

        public ICommand NextMonthCommand { get; set; }
        public ICommand PreviousMonthCommand { get; set; }

        private int _totalCups;
        public int TotalCups
        {
            get => _totalCups;
            set
            {
                _totalCups = value;
                OnPropertyChanged();
            }
        }

        private int _totalVolume;

        public int TotalVolume
        {
            get => _totalVolume;
            set
            {
                _totalVolume = value;
                OnPropertyChanged();
            }
        }

        public PagingViewModel()
        {
            CoffeeRecords = new ObservableCollection<Coffee>();

            NextMonthCommand = new RelayCommand(NextMonth);
            PreviousMonthCommand = new RelayCommand(PreviousMonth, CanGoPrevious);

            _currentMonth = DateTime.Now.Month;  // Default to the current month

            LoadCoffeeByMonth(_currentMonth); // Load the first page
        }

        private void LoadCoffeeByMonth(int month)
        {
            using (var dbContext = new CoffeeDbContext())
            {
                // Extract month from string date and filter based on the month
                var coffeeList = dbContext.Coffee
                    .AsEnumerable()  // Switch to client-side evaluation
                    .Where(c => GetMonthFromDateString(c.Date) == month)  // Filter by month on the client side
                    .ToList();

                CoffeeRecords.Clear();

                if (coffeeList.Any())
                {
                    foreach (var coffee in coffeeList)
                    {
                        CoffeeRecords.Add(coffee);
                    }

                    TotalCups = coffeeList.Count;
                    TotalVolume = coffeeList.Sum(c =>
                    {
                        if (int.TryParse(c.Volume, out int volume))
                        {
                            return volume;  // Return the parsed volume
                        }
                            return 0;      
                    });
                }
                else
                {
                    TotalCups = 0;
                    TotalVolume = 0;
                }
                OnPropertyChanged(nameof(CoffeeRecords));
            }
        }
        // Helper method to extract the month from a string date (format: dd/MM/yyyy)
        private int GetMonthFromDateString(string dateString)
        {
            if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate.Month;
            }
            else
            {
                MessageBox.Show($"Invalid date format: {dateString}");
            }
            return -1;  // Return -1 if parsing fails
        }

        private void NextMonth(object obj)
        {
            if (_currentMonth < 12)
            {
                _currentMonth++;  // Move to the next month
                MessageBox.Show($"Next Month: {_currentMonth}");  // Debugging
                LoadCoffeeByMonth(_currentMonth);  // Load coffee records for the new month
            }
        }

        private void PreviousMonth(object obj)
        {
            if (_currentMonth > 1)
            {
                _currentMonth--;  // Move to the previous month
                MessageBox.Show($"Previous Month: {_currentMonth}");  // Debugging
                LoadCoffeeByMonth(_currentMonth);  // Load coffee records for the previous month
            }
        }

        private bool CanGoPrevious(object obj)
        {
            return _currentMonth > 1;  // Only allow going to previous months if we're not on January
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
