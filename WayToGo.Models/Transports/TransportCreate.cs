using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayToGo.Models.Transports
{
    public class TransportCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Speed { get; set; }
        [Required]
        public double Cost { get; set; }
    }
}
