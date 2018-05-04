using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Celestial.Models
{
    [Table("Desinations")]
    public class Destination
    {
        [Key]
        public string City { get; set; }
        public string Country { get; set; }
        public int Id { get; set; }

        public Destination(string city, string country, int id = 0)
        {
            City = city;
            Country = country;
            Id = id;
        }

        public Destination() {  }
    }
}
