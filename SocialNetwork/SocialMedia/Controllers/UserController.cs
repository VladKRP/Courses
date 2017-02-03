using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using SocialMedia.Models;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;
using PagedList;

namespace SocialMedia.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var user = context.Users.SingleOrDefault(u => u.Id == currentUserId);
            if(user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        
        public ActionResult GetPeople()
        {
            return View();
        }

        public PartialViewResult GetPeoplePartial(string searchString, int? page)
        {

            if (searchString != null)
            {
                page = 1;
            }

            var currentUserId = User.Identity.GetUserId();
            var users = context.Users.Where(u => u.Id != currentUserId);
            var currentUserFriends = context.Friends.Where(u => u.UserId == currentUserId || u.UserFriendId == currentUserId);
            foreach(var friend in currentUserFriends)
            {
                users = users.Where(u => u.Id != friend.UserFriendId && u.Id != friend.UserId);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.FirstName.Contains(searchString)
                || u.LastName.Contains(searchString));
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            PagedList<ApplicationUser> userList = new PagedList<ApplicationUser>(users.ToList(), pageNumber, pageSize);
            return PartialView(userList);
        }

     
        [AllowAnonymous]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View("Index", user);
        }

        public FileContentResult GetImage(string userId)
        {
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == userId);
            if(user != null)
            {
                return File(user.ImageData, user.ImageMimeType);
            }
            else
            {
                return null;
            }
        }


        [HttpPost]
        public async Task<ActionResult> ChangeUserPhoto(HttpPostedFileBase image)
        {
            string currentUser = User.Identity.GetUserId();
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == currentUser);

            if (user == null)
            {
                return HttpNotFound();
            }

            if (image != null)
            {
                 user.ImageMimeType = image.ContentType;
                 user.ImageData = new byte[image.ContentLength];
                 image.InputStream.Read(user.ImageData, 0, image.ContentLength);
                 context.Entry(user).State = EntityState.Modified;
                 await context.SaveChangesAsync();
            }

            return RedirectToAction("Index");

        }
    }
}