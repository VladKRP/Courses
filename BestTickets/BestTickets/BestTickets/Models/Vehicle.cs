using System;
using System.Collections.Generic;

namespace BestTickets.Models
{
    public class Vehicle:IComparable<Vehicle>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Route { get; set; }
        
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }

        public IEnumerable<VehiclePlace> Places { get; set; }
        
        public int CompareTo(Vehicle obj)
        { 
            return 0;
        }
    }
}