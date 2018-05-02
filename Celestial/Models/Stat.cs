using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Celestial.Models
{
    [Table("Stats")]
    public class Stat
    {
        [Key]
        public int StatId { get; set; }
        public int Capital { get; set; }
        public int Crystal { get; set; }
        public int Pop { get; set; }
        public int Stability { get; set; }
        public int PlanetId { get; set; }
        public virtual Planet Planet { get; set; }
    }
}
