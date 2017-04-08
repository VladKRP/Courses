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
        public ActionResult Index(RouteViewModel route)
        {
            var tickets = RouteViewModel.FindTickets(route).ToList();
            return View("GetTickets", tickets);
        }

    }
}