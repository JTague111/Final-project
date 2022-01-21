using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayToGo.Models.Transports
{
    public class TransportDetail
    {
        public int TransportId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Speed { get; set; }
        public double Cost { get; set; }
    }
}
