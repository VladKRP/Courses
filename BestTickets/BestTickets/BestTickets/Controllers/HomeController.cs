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
            var tickets = TicketChecker.FindTickets(route).ToList();
            return View(tickets);
        }

    }
}