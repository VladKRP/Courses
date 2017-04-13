﻿using System;
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

        public IEnumerable<Tuple<string,string,string>> VehiclePlace { get; set; }

    }
}