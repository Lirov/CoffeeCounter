using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeCounter.Model
{
    public class CoffeeManager
    {
        public static ObservableCollection<Coffee> _DatabaseCoffee = new ObservableCollection<Coffee>() { new Coffee() { Kind = "Espresso", Volume = "80cc", Time = "16:45", Date = "01/09/2024", Location = "Office"} };
        public static ObservableCollection<Coffee> GetCoffee() { return _DatabaseCoffee; }

        public static void AddCoffee(Coffee coffee)
        {
            _DatabaseCoffee.Add(coffee);
        }
    }
}
