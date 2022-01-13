using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayToGo.Data
{
    public class Transport
    {
        [Key]
        public int TransportId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int SpeedInMPH { get; set; }
        [Required]
        public double CostPerMile { get; set; }

    }
}
