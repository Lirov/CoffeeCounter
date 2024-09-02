using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeCounter.Model
{
    public class Coffee
    {
        public Coffee() { }

        public int Id { get; set; }
        public string? Kind { get; set; }
        public string? Volume { get; set; }
        public string? Time { get; set; }
        public string? Date { get; set; }
        public string? Location { get; set; }

    }
}
