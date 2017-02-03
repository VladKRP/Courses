using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "User");
            }
            return View();
        }
    }
}