using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayToGo.Models
{
    public class RouteListItem
    {
        public int RouteId { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }

    }
}
