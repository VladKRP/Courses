using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SocialMedia.Models;
using Microsoft.AspNet.Identity;

namespace SocialMedia.Controllers
{
    public class UserWallPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public ActionResult Create(string postText)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = db.Users.SingleOrDefault(u => u.Id == currentUserId);
            if (currentUser == null)
            {
                return HttpNotFound();
            }

            if (postText == "")
            {
                return RedirectToAction("Index", "User");
            }
            UserWallPost userPost = new UserWallPost() { Text = postText, PostedDate = DateTime.Now, UserId = currentUserId, User = currentUser };
            
            db.UserWallPosts.Add(userPost);
            db.SaveChanges();
            return RedirectToAction("Index", "User");
        }

     
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserWallPost userWallPost = db.UserWallPosts.SingleOrDefault(p => p.Id == id);
            if(userWallPost != null)
            {
                db.UserWallPosts.Remove(userWallPost);
                db.SaveChanges();
            }
            return RedirectToAction("Index","User");
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
