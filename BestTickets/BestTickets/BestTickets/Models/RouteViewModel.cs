using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestTickets.Models
{
    public class RouteViewModel
    {
        public string DeparturePlace { get; set; }
        public string ArrivalPlace { get; set; }
        public DateTime Date { get; set; }

        public RouteViewModel()
        {
            Date = DateTime.Now.Date;
        }
    }
}