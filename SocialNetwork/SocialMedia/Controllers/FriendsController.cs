using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SocialMedia.Models;
using Microsoft.AspNet.Identity;
using PagedList;

namespace SocialMedia.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private IQueryable<Friend> IsConfirmRequest(bool isConfirmed)
        {
            var friends = db.Friends.Include(f => f.User).Include(f => f.UserFriend); 
            friends = friends.Where(f => f.IsConfirmed == isConfirmed);
            return friends;
        }

        private PagedList<Friend> GenerateFriendList(IQueryable<Friend> friends, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            PagedList<Friend> friendList = new PagedList<Friend>(friends.ToList(), pageNumber, pageSize);
            return friendList;
        }

        public ActionResult Index(int? page)
        {
            var currentUserId = User.Identity.GetUserId();
            var friends = IsConfirmRequest(true);
            friends = friends.Where((f => f.UserId == currentUserId || f.UserFriendId == currentUserId));
            return View(GenerateFriendList(friends,page));
        }

        public ActionResult IncomingRequests(int? page)
        {
            var currentUserId = User.Identity.GetUserId();
            var friends = IsConfirmRequest(false);
            friends = friends.Where((f => f.UserFriendId == currentUserId));
            return View(GenerateFriendList(friends, page));
        }

        public ActionResult OutgoingRequests(int? page)
        {
            var currentUserId = User.Identity.GetUserId();
            var friends = IsConfirmRequest(false);
            friends = friends.Where((f => f.UserId == currentUserId));
            return View(GenerateFriendList(friends, page));
        }

        public async Task<ActionResult> ConfirmRequest(string receiverId)
        {
            var currentUserId = User.Identity.GetUserId();
            if (receiverId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = db.Users.Find(receiverId);
            ApplicationUser userFriend = db.Users.Find(currentUserId);

            if (user != null && userFriend != null)
            {
                Friend friend = db.Friends.FirstOrDefault(f => f.UserId == receiverId && f.UserFriendId == currentUserId);
                friend.IsConfirmed = true;
                db.Entry(friend).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Friend friend = await db.Friends.FindAsync(id);
            if (friend == null)
            {
                return HttpNotFound();
            }
            return View(friend);
        }

        public async Task<ActionResult> Create(string id)
        {
            string currentUserId = User.Identity.GetUserId();
            var currentUser = db.Users.FirstOrDefault(u => u.Id == currentUserId);
            var userFriend = db.Users.FirstOrDefault(u => u.Id == id);
            Friend friend = new Friend()
            {
                User = currentUser,
                UserId = currentUserId,
                UserFriend = userFriend,
                UserFriendId = id,
            };

            if (ModelState.IsValid)
            {
                db.Friends.Add(friend);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("GetPeople","User");
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Friend friend = await db.Friends.FindAsync(id);
            if (friend != null)
            {
                db.Friends.Remove(friend);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
