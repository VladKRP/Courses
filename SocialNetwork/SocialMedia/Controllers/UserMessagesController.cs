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
    public class UserMessagesController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var userDialogs = context.Dialogs.Include(d => d.Receiver).Include(d => d.Sender).Include(d => d.Messages).Where(d => d.ReceiverId == currentUserId || d.SenderId == currentUserId);
            return View(userDialogs);
        }


        public ActionResult Details(string id)
        {
            var currentUserId = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.ReceiverId = id;
            var userMessages = context.UserMessages.Where(u => (u.ReceiverId == id && u.SenderId == currentUserId) || (u.ReceiverId == currentUserId && u.SenderId == id));
            userMessages = userMessages.OrderBy(d => d.PostedDate);
            if (userMessages == null)
            {
                return HttpNotFound();
            }
            return View(userMessages);
        }

        private Dialog findOrCreateDialog(ApplicationUser sender, ApplicationUser receiver)
        {
            Dialog existingDialog = context.Dialogs.SingleOrDefault(u => (u.SenderId == sender.Id && u.ReceiverId == receiver.Id) || (u.ReceiverId == sender.Id && u.SenderId == receiver.Id));
            if(existingDialog == null)
            {
                Dialog dialog = new Dialog() { Receiver = receiver, ReceiverId = receiver.Id, Sender = sender, SenderId = sender.Id };
                context.Dialogs.Add(dialog);
                context.SaveChanges();
                return dialog;
            }
            return existingDialog;  
        }

        private void SaveMessageToDialog(Dialog dialog, UserMessage message)
        {
            dialog.Messages.Add(message);
            context.SaveChanges();
        }

        private List<string> ChooseView(int? view, string receiverId)
        {
            string action = "";
            string controller = "";
            switch (view)
            {
                case 1:
                    controller = "User";
                    action = "GetPeople";
                    break;
                    
                case 2:
                    controller = "Friends";
                    action = "Index";
                    break;
                case 3:
                    controller = "UserMessages";
                    action = "Details/" + receiverId;
                    break;    
            }

            return (new List<string>() { controller, action});
        }


        public ActionResult Create(string id)
        {
            var user = context.Users.SingleOrDefault(u => u.Id == id);
            return View(new UserMessage {Receiver = user, ReceiverId = id });
        }


        [HttpPost]
        public async Task<ActionResult> Create(string receiverId, string messageText, int? view)
        {
            var currentUserId = User.Identity.GetUserId();
            var sender = await context.Users.SingleOrDefaultAsync(u => u.Id == currentUserId);
            var receiver = await context.Users.SingleOrDefaultAsync(u => u.Id == receiverId);
            if (receiver == null || sender == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            UserMessage message = new UserMessage()
            {
                Text = messageText,
                PostedDate = DateTime.Now,
                Receiver = receiver,
                ReceiverId = receiverId,
                Sender = sender,
                SenderId = currentUserId
            };
            var dialog = findOrCreateDialog(sender, receiver);
           
            context.UserMessages.Add(message);
            await context.SaveChangesAsync();

            SaveMessageToDialog(dialog, message);
            if(view != null)
            {
                List<string> redirectValues = ChooseView(view, receiverId);
                return RedirectToAction(redirectValues[1], redirectValues[0]);
            }
            return View("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
