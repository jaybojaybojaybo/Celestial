using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Celestial.Models
{
    [Table("Planets")]
    public class Planet
    {
        [Key]
        public int PlanetId { get; set; }
        public string Name { get; set; }
        public string Government { get; set; }
        public string Economy { get; set; }
        public string Geography { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int StatId { get; set; }
        public virtual ICollection<Stat> Stats { get; set; }
    }
}
