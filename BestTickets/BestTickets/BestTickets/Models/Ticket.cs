using System;
using System.Collections.Generic;

namespace BestTickets.Models
{
    public class Ticket
    {
        public string VehicleName { get; set; }
        public string VehicleType { get; set; }
        public string Route { get; set; }
        
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string TimeInWay { get; set; }

        //Implement method to calc TimeInWay

        public List<Tuple<string,int,int>> VehiclePlace { get; set; }
        //public int Price { get; set; }
        //public int FreePlaceAmount { get; set; }
    }
}