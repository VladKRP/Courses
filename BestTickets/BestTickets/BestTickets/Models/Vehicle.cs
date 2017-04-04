using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestTickets.Models
{
    public class Vehicle
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Route { get; set; }
        
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime Date { get; set; }

        public int Price { get; set; }
        public int FreePlaceAmount { get; set; }
    }
}