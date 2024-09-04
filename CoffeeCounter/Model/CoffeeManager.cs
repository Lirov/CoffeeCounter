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
        public static ObservableCollection<Coffee> _DatabaseCoffee = new ObservableCollection<Coffee>() {};
        public static ObservableCollection<Coffee> GetCoffee() { return _DatabaseCoffee; }

        public static void AddCoffee(Coffee coffee)
        {
            _DatabaseCoffee.Add(coffee);
        }
    }
}
