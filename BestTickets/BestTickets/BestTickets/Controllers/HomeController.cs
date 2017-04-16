using System.Linq;
using System.Web.Mvc;
using BestTickets.Models;
using System;

namespace BestTickets.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
             return View();
        }

        public ActionResult GetTickets(RouteViewModel route)
        {
            if (route.Date == null)
                route.Date = route.SetCurrentDate();
            return PartialView("_GetTickets", TicketChecker.FindTickets(route).ToList());
        }
    }
}