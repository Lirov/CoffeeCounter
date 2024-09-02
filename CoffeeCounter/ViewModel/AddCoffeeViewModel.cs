using CoffeeCounter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UsersIndex.Commands;

namespace CoffeeCounter.ViewModel
{
    public class AddCoffeeViewModel
    {
        public ICommand AddCoffeeCommand { get; set; }
        public string? Kind { get; set; }
        public string? Volume { get; set; }
        public string? Time { get; set; }
        public string? Date { get; set; }
        public string? Location { get; set; }

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
    }
}
