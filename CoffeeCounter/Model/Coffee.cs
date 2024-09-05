using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeCounter.Model
{
    public class Coffee
    {
        public Coffee() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Kind { get; set; }
        public string? Volume { get; set; }
        public string? Time { get; set; }
        public string? Date { get; set; }
        public string? Location { get; set; }

    }
}
