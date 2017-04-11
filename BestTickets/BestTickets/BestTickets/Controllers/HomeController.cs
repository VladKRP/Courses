using System.Linq;
using System.Web.Mvc;
using BestTickets.Models;

namespace BestTickets.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetTickets(RouteViewModel route)
        {
            //var tickets = TicketChecker.FindTickets(route).OrderBy(x => x.VehiclePlace.OrderBy(y => y.Item3));
            //return View(tickets);
            return View(TicketChecker.FindTickets(route).ToList());
        }

    }
}