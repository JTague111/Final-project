using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayToGo.Data
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Population { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
    }
}
